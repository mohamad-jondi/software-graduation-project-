using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class BaseRepositorie<T> : IBaseRepositories<T> where T : BaseEntity
    {
        private readonly DbContext _DbContext;
        private DbSet<T> _dbset;

        public BaseRepositorie(DbContext context)
        {
            _DbContext = context;
            _dbset = context.Set<T>();
        }
        public async Task<T> Add(T entity)
        {
            await _dbset.AddAsync(entity);
            await _DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRange(IEnumerable<T> entities)
        {
            await _dbset.AddRangeAsync(entities);
            await _DbContext.SaveChangesAsync();
            return entities;
        }

        public async Task Delete(T entity)
        {
            _dbset.Remove(entity);
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteRange(IEnumerable<T> entites)
        {
             _dbset.RemoveRange(entites);
            await _DbContext.SaveChangesAsync();
        }

        public  IQueryable<T> Get()
        {
            return _dbset.AsQueryable();
        }

        public IQueryable<T> GetPaginated(int pageNumber, int pageSize)
        {
            return _dbset.Skip(pageSize * (pageNumber-1)).Take(pageSize).AsQueryable();
        }

        public async Task<T> Update(T entity)
        {
            _dbset.Update(entity);
            await _DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> UpdateRange(IEnumerable<T> entity)
        {
            _dbset.UpdateRange(entity);
            await _DbContext.SaveChangesAsync();
            return entity;
        }
    }
}
