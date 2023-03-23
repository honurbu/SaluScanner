﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaluScanner.Core.DTOs;
using SaluScanner.Core.Entities;
using SaluScanner.Core.Services;
using SaluScanner.Repository.DbContexts;
using System.Reflection.Metadata;
using System.Security.Claims;
using static System.Reflection.Metadata.BlobBuilder;

namespace SaluScanner.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : CustomBaseController
    {
        private readonly IUserAllergenService _userAllergenService;
        //private readonly UserManager<User> _userManager;
        private readonly User _user;
        private readonly IProductService _service;

     

        SqlServerDbContext dbContext;

        public TestController(IUserAllergenService userAllergenService, SqlServerDbContext dbContext, User user, IProductService _service)
        {
            _userAllergenService = userAllergenService;
            this.dbContext = dbContext; 
            _user = user;
            this._service = _service;
        }

//UserManager<User> userManager, 
//_userManager = userManager;


        [Authorize]
        [HttpGet]
        public IActionResult GetAllergy()
        {
            var userName = HttpContext.User.Identity.Name;
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            var users = dbContext.Users.Where(p => p.Id == userIdClaim.Value).Include(p => p.Allergies);
            //var users = dbContext.Users.Where(p => p.Id == userIdClaim.Value).Include(p => p.Allergies);
            return Ok(users.FirstOrDefault());

        }


        [Authorize]
        [HttpGet("{barcode}")]
        public async Task<IActionResult> GetByProductWithAllergens(String barcode)
        {
            var products = dbContext.Products.Where(x => x.Barcode == barcode).Select(x => x.Contents.Select(x=>x.AllergenId));

            var userName = HttpContext.User.Identity.Name;
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            var users = dbContext.Users.Where(p => p.Id == userIdClaim.Value).Include(p => p.Allergies).Select(p => p.Allergies);


            if (products==users)
            {
                return Ok("Tüketme");
            }


            return ActionResultInstance(await _service.GetProductByBarcodeAsync(barcode));
        }




        //[Authorize]
        //[HttpPut]
        //public async Task<IActionResult> EditProfileAsync(UserUpdateViewModel model)
        //{
        //    var userName = HttpContext.User.Identity.Name;
        //    var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;



        //    var values = await _userManager.FindByNameAsync(User.Identity.Name);
        //    values.Email = model.mail;
        //    values.UserName = model.userName;
        //    values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, model.Ppassword);
        //    values.Height = model.height;
        //    values.Weight = model.weight;
        //    var result = await _userManager.UpdateAsync(values);


        //    return Ok(result);

        //}


        //[Authorize]
        //[HttpPut]
        //public async Task<IActionResult> ProfileEdit(UserUpdateDto user)
        //{
        //    var userName = HttpContext.User.Identity.Name;
        //    var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;


        //    var values = await _userManager.FindByNameAsync(User.Identity.Name);

        //    values.Email = user.Email;
        //    values.UserName = user.UserName;
        //    values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, user.Password);
        //    values.Height = user.height;
        //    values.Weight = user.weight;
        //    var result = await _userManager.UpdateAsync(values);

        //    return Ok(values);


        //}

    }
}
