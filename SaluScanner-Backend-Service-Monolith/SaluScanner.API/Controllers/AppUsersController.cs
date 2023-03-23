using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaluScanner.AuthServer.Core.Service;
using SaluScanner.Core.Entities;
using SaluScanner.Core.Services;
using SaluScanner.Repository.DbContexts;
using System.Linq;
using System.Security.Claims;

namespace SaluScanner.API.Controllers
{
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
        public async Task<IActionResult> GetByProductWithAllergens(String barcode)
        {
            var products = await dbContext.Products.Where(x => x.Barcode == barcode).Select(x => x.Contents.Select(x => x.AllergenId)).FirstOrDefaultAsync();

            var userName = HttpContext.User.Identity.Name;
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            var allergy = await dbContext.Users.Where(p => p.Id == userIdClaim.Value).Include(p => p.Allergies).Select(p => p.Allergies.Select(p => p.Id)).FirstOrDefaultAsync();


            //bool controls = products.Any(x => allergy.Contains((IEnumerable<int>)x)); // true


            //if (controls == true)
            //{
            //    return Ok("Alerji var");
            //}
            //else
            //{
            //    return Ok("Alerji yok");
            //}

            if(products.FirstOrDefault() == allergy.FirstOrDefault())
            {
                return Ok("alerji var");
            }
            else
            {
                return Ok("alerji yok");
            }


        }



        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> AddAllergen(int id)
        //{
        //    var userName = HttpContext.User.Identity.Name;
        //    var userIdClaim = User.Claims.FirstOrDefault(x=>x.Type== ClaimTypes.NameIdentifier).Value;


        //    var userAllergy = await dbContext.Users.Where(p=>p.Id == userIdClaim).Include(p=>p.Allergies).Select(p=>p.Allergies.Select(p=>p.Id))
        //}



        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> AddAllergen(int id)
        //{

        //    var userName = HttpContext.User.Identity.Name;
        //    var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);


        //    //var userallergy = await dbContext.Users.Where(p => p.Id == userIdClaim.Value).Include(p => p.Allergies).Select(p => p.Allergies.Select(p => p.Id)).FirstOrDefaultAsync();

        //    var allergy = dbContext.Allergens.Select(a => a.Id);

        //    foreach (var item in allergy)
        //    {
        //        if(id == item)
        //        {
        //            var s = dbContext.Users.Include(u => u.Allergies).ThenInclude(d=>d.Id);
        //        }
        //        else
        //        {

        //        }
        //    }



        //    return Ok();

        //}

    }
}



