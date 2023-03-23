using Microsoft.AspNetCore.Mvc;
using SaluScanner.Core.Services;
using SaluScanner.Repository.DbContexts;

namespace SaluScanner.WEB.Controllers
{
    public class AllergenController : Controller
    {
        //private readonly SqlServerDbContext _context;
        private readonly IAllergenService _allergenService;

        public AllergenController(IAllergenService allergenService)
        {
            //_context = context;
            _allergenService = allergenService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _allergenService.GetAllAsync());
        }
    }
}
