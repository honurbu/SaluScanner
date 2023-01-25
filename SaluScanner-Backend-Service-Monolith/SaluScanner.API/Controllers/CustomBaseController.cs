using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaluScanner.SharedLibrary.DTOs;

namespace SaluScanner.API.Controllers
{
    [ApiController]

    public class CustomBaseController : ControllerBase
    {
        public IActionResult ActionResultInstance<T>(Response<T> response) where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
