using BrewAPI.Data;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

// Generic repository
// Expect a generic <T> entity

namespace BrewAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        protected readonly BrewAPIDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(BrewAPIDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            // Find IEntity.Id insteed of dbSet primary key
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }
        public virtual async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            // Attach entity and mark as modified before saving to DB
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var rowsAffected = await _dbSet.Where(e => e.Id == id).ExecuteDeleteAsync();
            // If more than 0 rows affected, return true
            return rowsAffected > 0;
        }
    }
}