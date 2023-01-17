using SaluScanner.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity, new()
    {
        Task<TEntity> GetByIdAsync(int id);
        // productRepository.Where(x => x.id > 5) = IQueryable, no SQL query yet.
        // productRepository.Where(x => x.id > 5).OrdeyBy(..) = IQueryable, no SQL query yet.
        // * productRepository.Where(x => x.id > 5).ToList() = List, SQL has commited.
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
        Task AddAsync(TEntity entity);
        Task AddRangeAsyn(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
