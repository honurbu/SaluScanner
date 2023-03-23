using SaluScanner.Core.Repositories;
using SaluScanner.Core.Services;
using SaluScanner.Repository.Repositories;
using SaluScanner.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


builder.Services.AddScoped<IAllergenRepository, AllergenRepository>();
builder.Services.AddScoped<IAllergenService, AllergenService>();

//builder.Services.AddDbContext<SaluScanner.Repository.DbContexts.SqlServerDbContext>();

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
