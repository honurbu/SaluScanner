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
    public class ProductController : ControllerBase
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
            var products = await _service.GetAllAsync();

            return Ok(products);
        }

         /*
         [HttpGet("{barcode}")]
         public async Task<IActionResult> GetByBarcode(String barcode)
         {
             var product = await _service.GetProductByBarcodeAsync(barcode);

             return Ok(product);
         }
         */


        //[HttpGet("{barcode}")]
        //public async Task<IActionResult> GetByCertificateProduct(String barcode)
        //{
        //    var certificateProducts = await _service.GetCertificateByProductWithBarcodeAsync(barcode);
        //    return Ok(certificateProducts);
        //}


        [HttpPost]
        public async Task<IActionResult> Add(ProductDto productDto)
        {
            await _service.AddAsync(productDto);

            return Ok(productDto);
        }

    }
}
