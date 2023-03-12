using Microsoft.EntityFrameworkCore;
using SaluScanner.AuthServer.Core.Service;
using SaluScanner.Core.Repositories;
using SaluScanner.Core.Services;
using SaluScanner.Core.UnitOfWorks;
using SaluScanner.Repository.DbContexts;
using SaluScanner.Repository.Repositories;
using SaluScanner.Repository.UnitOfWorks;
using SaluScanner.Service.Mapping;
using SaluScanner.Service.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();




// Unit of Work Dependency Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repository Layer Dependency Injection
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
builder.Services.AddScoped(typeof(ICompanyRepository), typeof(CompanyRepository));

// Service Layer Dependency Injection
//builder.Services.AddScoped(typeof(IGenericService<TEntity, TDto>), typeof(GenericService<>));
builder.Services.AddScoped(typeof(IProductService), typeof(ProductService));
builder.Services.AddScoped(typeof(ICompanyService), typeof(CompanyService));


//

//
builder.Services.AddAutoMapper(typeof(MapProfile));
//
builder.Services.AddDbContext<SqlServerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDbContext"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("SaluScanner.Repository");
    });
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

});

//
builder.Services.AddDbContext<SqlServerDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDbContext"), option =>
    {
        // Get me Assembly (App) that has SqlServerDbContext in it.
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(SqlServerDbContext)).GetName().Name);
    });
});

//






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
