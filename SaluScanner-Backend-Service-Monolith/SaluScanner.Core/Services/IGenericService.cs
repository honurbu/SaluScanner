using SaluScanner.Core.Entities;
using SaluScanner.SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.Services
{
    public interface IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<Response<TDto>> GetByIdAsync(int id);
        // this is business layer so there is no need for IQueryable anymore after this layer.
        Task<Response<IEnumerable<TDto>>> GetAllAsync();
        Task<Response<TDto>> AddAsync(TDto entity);
        Task<Response<NoDataDto>> RemoveById(int id);
        Task<Response<NoDataDto>> Update(TDto entity);
    }
}
