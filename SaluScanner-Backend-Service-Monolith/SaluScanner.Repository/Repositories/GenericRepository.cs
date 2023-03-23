using Microsoft.EntityFrameworkCore;
using SaluScanner.Core.Entities;
using SaluScanner.Core.Repositories;
using SaluScanner.Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> 
        where TEntity : class, IEntity, new()
    {
        protected readonly SqlServerDbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        public GenericRepository(SqlServerDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsyn(IEnumerable<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await dbSet.AnyAsync(expression);
        }

        public IQueryable<TEntity> GetAll()
        {
            // take data but no track them. Like pass-by-value injecting
            //      Example
            var products = dbContext.Products.Include(p => p.Contents).Include(p => p.Nutrition).AsQueryable();
            var contents = dbContext.Contents.Include(c => c.Allergen);
            return (IQueryable<TEntity>)products;
            //      Example
            //return dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return dbSet.Where(expression);
        }
    }
}
