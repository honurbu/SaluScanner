using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaluScanner.Core.Services;
using SaluScanner.Repository.DbContexts;
using System.Security.Claims;

namespace SaluScanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IUserAllergenService _userAllergenService;
        SqlServerDbContext dbContext;

        public TestController(IUserAllergenService userAllergenService, SqlServerDbContext dbContext)
        {
            _userAllergenService = userAllergenService;
            this.dbContext = dbContext;
        }





        [Authorize]
        [HttpGet]
        public IActionResult GetAllergy()
        {
            var userName = HttpContext.User.Identity.Name;
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);



            var users = dbContext.Users.Where(p=>p.Id == userIdClaim.Value).Include(p=>p.Allergies);





            return Ok(users.FirstOrDefault());

        }



    }
}
