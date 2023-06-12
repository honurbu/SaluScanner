using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaluScanner.AuthServer.Core.Service;
using SaluScanner.Core.Entities;
using SaluScanner.Core.Services;
using SaluScanner.Repository.DbContexts;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using static Azure.Core.HttpHeader;

namespace SaluScanner.API.Controllers
{
    [EnableCors]
    [Route("[controller]/[action]")]
    [ApiController]
    public class AppUsersController : CustomBaseController
    {
        private readonly IUserAllergenService _userAllergenService;
        private readonly IProductService _service;

        SqlServerDbContext dbContext;

        public AppUsersController(IUserAllergenService _userAllergenService, SqlServerDbContext dbContext, IProductService service)
        {
            this._userAllergenService = _userAllergenService;
            this.dbContext = dbContext;
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllergy()
        {
            var userName = HttpContext.User.Identity.Name;
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);


            var users = dbContext.Users.Where(p => p.Id == userIdClaim.Value)
                .Include(p => p.Allergies)
                .Select(p => p.Allergies);

            return Ok(users.FirstOrDefault());
        }




        [Authorize]
        [HttpGet("{barcode}")]
        public async Task<IActionResult> GetByProductWithAllergens(string barcode)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var productControl = await dbContext.Products.Where(p => p.Barcode == barcode).FirstOrDefaultAsync();


            if (productControl == null)
            {
                return BadRequest("Barcode Not Found !");
            }

             var productContents = dbContext.Products
                  .Where(p => p.Barcode == barcode)
                  .Include(p => p.Contents)
                  .ThenInclude(p => p.Allergen)
                  .FirstOrDefault().Contents.ToList();

            var user = await dbContext.Users
                .Where(u => u.Id == userIdClaim)
                .Include(u => u.Allergies)
                .FirstOrDefaultAsync();


            foreach (var allergen in user.Allergies)
            {
                foreach (var content in productContents)
                {
                    if (allergen.Id == content.AllergenId)
                    {
                        return Ok("Alerjin var tüketme ölersin");
                    }
                }
            }
            return Ok("Yok rahatlıkla ye sen daha yaşarsın bizi de gömersin");
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAllergen(int id)
        {
            var allergy = await dbContext.Allergens.FirstOrDefaultAsync(x => x.Id == id);
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userIdClaim);

            if (user == null)
            {
                return BadRequest("User not found !");
            }

            if (allergy == null)
            {
                return BadRequest("Allergy id not Founds");
            }


            user.Allergies.Add(allergy);
            await dbContext.SaveChangesAsync();
            return Ok("Allergens Added !");

        }

    }
}



