using AutoMapper;
using SaluScanner.Core.DTOs;
using SaluScanner.Core.Entities;
using SaluScanner.Core.Repositories;
using SaluScanner.Core.Services;
using SaluScanner.Core.UnitOfWorks;
using SaluScanner.Service.Mapping;
using SaluScanner.SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Service.Services
{
    public class ProductService : GenericService<Product, ProductDto>, IProductService
    {
        protected readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> _genericRepository, IUnitOfWork _unitOfWork, IProductRepository _productRepository, IMapper mapper) : base(_genericRepository, _unitOfWork)
        {
            this._repository = _productRepository;
            _mapper = mapper;
        }

        public async Task<Response<Product>> GetProductByBarcodeAsync(string barcode)
        {
            var entity = await _repository.GetProductByBarcodeAsync(barcode);

            if (entity == null)
            {
                return Response<Product>.Fail("Barcode Not Found", 404, true);
            }

            var dto = ObjectMapper.Mapper.Map<Product>(entity);

            return Response<Product>.Success(dto, 200);
        }
    }
}