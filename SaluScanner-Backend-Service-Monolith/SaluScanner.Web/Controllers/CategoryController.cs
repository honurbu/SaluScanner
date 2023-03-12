using Microsoft.AspNetCore.Mvc;
using SaluScanner.Core.Entities;
using SaluScanner.Repository.DbContexts;

namespace SaluScanner.Web.Controllers
{
    public class CategoryController : Controller
    {
        SqlServerDbContext context = new SqlServerDbContext();

        
        public IActionResult Index()
        {
            var values = context.Categories.ToList();
            return View(values);
        }

        
        
        public async Task<IActionResult> Add(Category category)
        {
            if (category.CategoryName == null)
            {
                return View();
            }
            context.Categories.AddAsync(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        
        
        public IActionResult Remove(int id)
        {
            var values = context.Categories.Find(id);
            context.Remove(values);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        
        [HttpGet]
        public IActionResult Update(int id)
        {
            var values = context.Categories.Find(id);
            return View(values);
        }

        
        
        [HttpPost]
        public IActionResult Update(Category category)
        {
            var values = context.Categories.Find(category.Id);
            values.CategoryName = category.CategoryName;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
