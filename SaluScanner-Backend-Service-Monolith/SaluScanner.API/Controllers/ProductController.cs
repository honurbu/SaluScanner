using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaluScanner.Core.DTOs;
using SaluScanner.Core.Entities;
using SaluScanner.Core.Services;
using System.Collections.Generic;

namespace SaluScanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : CustomBaseController
    {
        private readonly IProductService _service;

        public ProductController(IProductService _service)
        {
            this._service = _service;
        }

        // Just an example
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ActionResultInstance(await _service.GetAllAsync());
        }

         
         [HttpGet("{barcode}")]
         public async Task<IActionResult> GetByBarcode(String barcode)
         {
            return ActionResultInstance(await _service.GetProductByBarcodeAsync(barcode));
         }




        [HttpPost]
        public async Task<IActionResult> Add(ProductDto productDto)
        {
            await _service.AddAsync(productDto);

            return Ok(productDto);
        }

    }
}
