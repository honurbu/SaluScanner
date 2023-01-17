using Microsoft.EntityFrameworkCore;
using SaluScanner.Core.Entities;
using SaluScanner.Core.Repositories;
using SaluScanner.Repository.DbContexts;

namespace SaluScanner.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(SqlServerDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Product> GetProductByBarcodeAsync(string barcode)
        {
            var filteredProduct = dbContext.Products.Where(p => p.Barcode == barcode)
                .Include(p => p.Nutrition)
                .Include(p => p.Certificates)
                .Include(p => p.ProductDetail);

           // Console.WriteLine("--------------Certificate Count: "+filteredProduct.First().Certificates.Count);

            return await filteredProduct.FirstOrDefaultAsync();
        }
    }
}
