using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaluScanner.AuthServer.Core.Service;
using SaluScanner.Core.Entities;
using SaluScanner.Core.Services;
using SaluScanner.Repository.DbContexts;
using System.Security.Claims;

namespace SaluScanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : CustomBaseController
    {
        private readonly IUserAllergenService _userAllergenService;
        SqlServerDbContext dbContext;

        public AppUsersController(IUserAllergenService _userAllergenService,SqlServerDbContext dbContext)
        {
            this._userAllergenService = _userAllergenService;
            this.dbContext = dbContext;
        }


        //[Authorize]
        //[HttpGet]
        //public async Task<IActionResult> GetUserAllergen()
        //{
        //    return ActionResultInstance(await _userAllergenService.GetAllergenByUser());
        //}


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllergy()
        {
            var userName = HttpContext.User.Identity.Name;
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);


            var users = dbContext.Users.Where(p => p.Id == userIdClaim.Value).Include(p => p.Allergies);

            return Ok(users.FirstOrDefault());
        }



    }
}



