using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaluScanner.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Repository.DbContexts
{
    public class SqlServerDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Nutrition> Nutritions { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Company> Companies { get; set; }

        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=104.247.162.242\\MSSQLSERVER2019;Initial Catalog=saluscan_salus; Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; user=saluscan_onur; password=Saluscanner123.");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Search "Configurations" in the current Assembly (application)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
