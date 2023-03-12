using Microsoft.AspNetCore.Mvc;
using SaluScanner.Repository.DbContexts;
using SaluScanner.Web.Models;
using System.Diagnostics;

namespace SaluScanner.Web.Controllers
{
    public class HomeController : Controller
    {
        SqlServerDbContext context =new SqlServerDbContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var values =  context.Products.ToList();
            return View(values);
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}