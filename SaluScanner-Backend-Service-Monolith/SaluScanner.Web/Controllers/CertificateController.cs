using Microsoft.AspNetCore.Mvc;
using SaluScanner.Core.Entities;
using SaluScanner.Repository.DbContexts;

namespace SaluScanner.Web.Controllers
{
    public class CertificateController : Controller
    {
        SqlServerDbContext context = new();
        
        
        public IActionResult Index()
        {
            var values = context.Certificates.ToList();
            return View(values);
        }



        public async Task<IActionResult> Add(Certificate certificate)
        {
            if (certificate.CertificateName == null)
            {
                return View();
            }
            context.Certificates.AddAsync(certificate);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Remove(int id)
        {
            var values = context.Certificates.Find(id);
            context.Remove(values);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        
        
        
        [HttpGet]
        public IActionResult Update(int id)
        {
            var values = context.Certificates.Find(id);
            return View(values);
        }


        
        
        [HttpPost]
        public IActionResult Update(Certificate certificate)
        {
            var values = context.Certificates.Find(certificate.Id);
            values.CertificateName = certificate.CertificateName;
            values.Description= certificate.Description;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
