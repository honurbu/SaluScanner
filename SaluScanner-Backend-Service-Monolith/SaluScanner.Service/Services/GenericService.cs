using Microsoft.EntityFrameworkCore;
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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Service.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> 
        where TEntity : class, IEntity, new()
        where TDto : class, IDto, new()
    {
        protected readonly IGenericRepository<TEntity> _genericRepository;
        protected readonly IUnitOfWork _unitOfWork;

        public GenericService(IGenericRepository<TEntity> _genericRepository, IUnitOfWork _unitOfWork)
        {
            this._genericRepository = _genericRepository;
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Response<TDto>> AddAsync(TDto dto)
        {
            var entity = ObjectMapper.Mapper.Map<TEntity>(dto);

            await _genericRepository.AddAsync(entity);

            await _unitOfWork.CommitAsync();

            return Response<TDto>.Success(dto,200);

        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var dtos = ObjectMapper.Mapper.Map<List<TDto>>(await _genericRepository.GetAll().ToListAsync()); 

            return Response<IEnumerable<TDto>>.Success(dtos, 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);

            var dto = ObjectMapper.Mapper.Map<TDto>(entity);

            return Response<TDto>.Success(dto,200);
        }

        public async Task<Response<NoDataDto>> RemoveById(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);

            if(entity == null)
            {
                return Response<NoDataDto>.Fail("ID Not Found", 404);
            }

            _genericRepository.Remove(entity);

            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<NoDataDto>> Update(TDto dto)
        {
            var entity = ObjectMapper.Mapper.Map<TEntity>(dto);

            if (entity == null)
            {
                return Response<NoDataDto>.Fail("Not Found", 404);
            }

            _genericRepository.Update(entity);

            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(204);
        }
    }
}
