using BrewAPI.Data;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

// This repository extends the GenericRepository class 
// CRUD operations are inherited from GenericRepository
// Override specific methods and add additional methods to handle specific business logic

namespace BrewAPI.Repositories
{
    public class MenuItemRepository : GenericRepository<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(BrewAPIDbContext context) : base(context)
        {
        }

        public override async Task<MenuItem?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.PK_MenuItemId == id);
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var rowsAffected = await _dbSet.Where(m => m.PK_MenuItemId == id).ExecuteDeleteAsync();
            return rowsAffected > 0;
        }

        public async Task<List<MenuItem>> GetMenuItemByCategoryAsync(string category)
        {
            return await _dbSet
                .Where(m => m.Category != null && m.Category.ToLower() == category.ToLower())
                .ToListAsync();
        }
    }
}