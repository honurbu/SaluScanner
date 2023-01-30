using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SaluScanner.Core.Repositories;
using SaluScanner.Core.Services;
using SaluScanner.Core.UnitOfWorks;
using SaluScanner.Repository.DbContexts;
using SaluScanner.Repository.Repositories;
using SaluScanner.Repository.UnitOfWorks;
using SaluScanner.Service.Mapping;
using SaluScanner.Service.Services;
using SaluScanner.SharedLibrary.Extensions;
using SaluScanner.SharedLibrary.Token;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
// Add services to the container.

// Modified with AddJsonOptions to avoiding Cycle Reference Error!
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Unit of Work Dependency Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repository Layer Dependency Injection
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
builder.Services.AddScoped(typeof(IUserAllergenRepository), typeof(UserAllergenRepository));

// Service Layer Dependency Injection
//builder.Services.AddScoped(typeof(IGenericService<TEntity, TDto>), typeof(GenericService<>));
builder.Services.AddScoped(typeof(IProductService), typeof(ProductService));
builder.Services.AddScoped(typeof(IUserAllergenService), typeof(UserAllergenService));

//AutoMapper
builder.Services.AddAutoMapper(typeof(MapProfile));

// Swagger
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SqlServerDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDbContext"), option =>
    {
        // Get me Assembly (App) that has SqlServerDbContext in it.
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(SqlServerDbContext)).GetName().Name);
    });
});


//builder.Services.AddDbContext<SqlServerDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDbContext"), sqlOptions =>
//    {
//        sqlOptions.MigrationsAssembly("SaluScanner.Repository");
//    });
//    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

//});



builder.Services.Configure<CustomTokenOption>(configuration.GetSection("TokenOption"));
var tokenOptions = configuration.GetSection("TokenOption").Get<CustomTokenOption>();

builder.Services.AddCustomTokenAuth(tokenOptions);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
