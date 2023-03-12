using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaluScanner.Core.Entities;
using SaluScanner.Repository.DbContexts;

namespace SaluScanner.Web.Controllers
{
    public class AllergenController : Controller
    {
        SqlServerDbContext context = new SqlServerDbContext();


        public IActionResult Index()
        {
            var values = context.Allergens.ToList();
            return View(values);
        }



        public async Task<IActionResult> Add(Allergen allergen)
        {
            if (allergen.AllergenName == null)
            {
                return View();
            }
            context.Allergens.AddAsync(allergen);
            context.SaveChanges();
            return RedirectToAction("Index");
        }



        public IActionResult Remove(int id)
        {
            var values = context.Allergens.Find(id);
            context.Remove(values);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        
        [HttpGet]
        public IActionResult Update(int id)
        {
            var values = context.Allergens.Find(id);
            return View(values);
        }




        [HttpPost]
        public IActionResult Update(Allergen allergen)
        {
            var values = context.Allergens.Find(allergen.Id);
            values.AllergenName = allergen.AllergenName;
            values.AllergenDescription = allergen.AllergenDescription;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
