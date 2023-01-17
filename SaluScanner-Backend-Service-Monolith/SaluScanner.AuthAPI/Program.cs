using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SaluScanner.AuthServer.Core.Service;
using SaluScanner.Core.Configuration;
using SaluScanner.Core.Entities;
using SaluScanner.Core.Repositories;
using SaluScanner.Core.Service;
using SaluScanner.Core.Services;
using SaluScanner.Core.UnitOfWorks;
using SaluScanner.Repository.DbContexts;
using SaluScanner.Repository.Repositories;
using SaluScanner.Repository.UnitOfWorks;
using SaluScanner.Service.Services;
using SaluScanner.SharedLibrary.Token;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// DI Register
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// DI Register of Generics (careful to multi type GenericService! It uses "," for each generic type)
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<,>),typeof(GenericService<,>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// DbContext
builder.Services.AddDbContext<SqlServerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDB"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("SaluScanner.Repository");
    });
});


// User Identity and User Roles(default by IdentityRole) but we could made a new one for Role like UserRole class etc.
builder.Services.AddIdentity<User, IdentityRole>(Opt =>
{
    Opt.User.RequireUniqueEmail= true;
    Opt.Password.RequireNonAlphanumeric= false;
}).AddEntityFrameworkStores<SqlServerDbContext>().AddDefaultTokenProviders();


// Options Pattern !
// DI of CustomTokenOption object. Take what it needs from appsettings.json
builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
builder.Configuration.GetSection("Clients").Get<List<Client>>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
