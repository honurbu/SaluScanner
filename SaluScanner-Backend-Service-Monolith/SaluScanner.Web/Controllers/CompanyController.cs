using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaluScanner.Repository.DbContexts;
using SaluScanner.Core.Entities;
using SaluScanner.Core.DTOs;
using SaluScanner.Core.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace SaluScanner.Web.Controllers
{
    public class CompanyController : Controller
    {
        SqlServerDbContext context = new SqlServerDbContext();

        // private readonly ICompanyService _companyService;
        //public CompanyController(ICompanyService companyService)
        //{
        //    _companyService = companyService;
        //}


        public async Task<IActionResult> Index()
        {
            var values = await context.Companies.ToListAsync();
            return View(values);

        }



        public async Task<IActionResult> Add(Company company)
        {
            if (company.About == null)
            {
                return View();
            }
            await context.Companies.AddAsync(company);
            context.SaveChanges();
            return RedirectToAction("Index");
        }



        
        public IActionResult Remove(int id)
        {
            var values = context.Companies.Find(id);
            context.Remove(values);
            context.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var values = context.Companies.Find(id);
            return View(values);

        }

        
        
        
        [HttpPost]       
        public IActionResult Update(Company company)
        {
            var values = context.Companies.Find(company.Id);
            values.Name = company.Name;
            values.About = company.About;
            values.Mail = company.Mail;
            values.ContactNumber = company.ContactNumber;
            values.Website999 = company.Website999;
            context.SaveChanges();
            return RedirectToAction("Index");
        }















    }
}
