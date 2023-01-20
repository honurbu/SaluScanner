using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using System.Configuration;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment= builder.Environment;

// Add services to the container.
// DI Register
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// DI Register of Generics (careful to multi type GenericService! It uses "," for each generic type)
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// DbContext
builder.Services.AddDbContext<SqlServerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDbContext"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("SaluScanner.Repository");
    });
});


// User Identity and User Roles(default by IdentityRole) but we could made a new one for Role like UserRole class etc.
builder.Services.AddIdentity<User, IdentityRole>(Opt =>
{
    Opt.User.RequireUniqueEmail = true;
    Opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<SqlServerDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<CustomTokenOption>(configuration.GetSection("TokenOption"));
builder.Services.Configure<List<Client>>(configuration.GetSection("Clients"));



builder.Services.AddAuthentication(options => {

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt => {

    var tokenOptions = configuration.GetSection("TokenOption").Get<CustomTokenOption>();

    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),


        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
