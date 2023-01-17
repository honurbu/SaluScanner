using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaluScanner.Core.DTOs;
using SaluScanner.Core.Service;

namespace SaluScanner.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenAsync(LoginDto loginDto)
        {
            var results = await _authenticationService.CreateTokenAsync(loginDto);

            return ActionResultInstance(results);
        }

        [HttpPost]
        public IActionResult CreateTokenByNonUser(NonUserLoginDto nonUserLoginDto)
        {
            var results = _authenticationService.CreateTokenByNonUser(nonUserLoginDto);
            return ActionResultInstance(results);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            var results = await _authenticationService.CreateTokenByRefreshTokenAsync(refreshToken);
            return ActionResultInstance(results);
        }


        [HttpPost]
        public async Task<IActionResult> RevokeRefreshTokenAsync(string refreshToken)
        {
            var results = await _authenticationService.RevokeRefreshTokenAsync(refreshToken);
            return ActionResultInstance(results);
        }
    }
}
