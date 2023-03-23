using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaluScanner.AuthServer.Core.Service;
using SaluScanner.Core.DTOs;

namespace SaluScanner.AuthAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            try
            {
                return ActionResultInstance(await _userService.CreateUserAsync(createUserDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return ActionResultInstance(await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name));
        }

    }
}
