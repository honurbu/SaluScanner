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

        public async Task<Response<ProductDto>> GetProductByBarcodeAsync(string barcode)
        {
            var entity = await _repository.GetProductByBarcodeAsync(barcode);

            if(entity == null)
            {
                return Response<ProductDto>.Fail("Barcode Not Found", 404);
            }

            var dto = ObjectMapper.Mapper.Map<ProductDto>(entity);

            return Response<ProductDto>.Success(dto, 200);
        }
    }
}