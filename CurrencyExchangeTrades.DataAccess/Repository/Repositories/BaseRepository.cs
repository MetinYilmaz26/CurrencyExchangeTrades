using CurrencyExchangeTrades.DataAccess.Repository.Interfaces;
using CurrencyExchangeTrades.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeTrades.DataAccess.Repository.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbContext _context;
        protected DbSet<TEntity> _dbSet;
        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<int> Add(TEntity entity)
        {
            _dbSet.Add(entity);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddRange(TEntity[] entities)
        {
            _dbSet.AddRange(entities);
            return await _context.SaveChangesAsync();
        }
        public async void Delete(int id)
        {
            _dbSet.Remove(Get(id).Result);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> Get(int id, string[]? navigationPropertyPaths = null)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();
            if (navigationPropertyPaths != null)
            {
                foreach (var path in navigationPropertyPaths)
                {
                    query = query.Include(path);
                }
            }
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }


        public virtual async Task<List<TEntity>> GetAll(string[]? navigationPropertyPaths = null)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();
            if (navigationPropertyPaths != null)
            {
                foreach (var path in navigationPropertyPaths)
                {
                    query = query.Include(path);
                }
            }
            return await query.ToListAsync();
        }

    }
}
