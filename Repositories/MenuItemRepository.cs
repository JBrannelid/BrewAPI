using BrewAPI.Data;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BrewAPI.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly BrewAPIDbContext _context;

        public MenuItemRepository(BrewAPIDbContext context)
        {
            _context = context;
        }

        // Adds a new menu item to the Db and returns the primary key (Id) of the created item
        public async Task<int> CreateMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();
            return menuItem.PK_MenuItemId;
        }

        // Deletes a menu item by Id. Returns true if a row was affected. False if no row was affected
        public async Task<bool> DeleteMenuItemAsync(int menuItemId)
        {
            var rowsAffected = await _context.MenuItems.Where(m => m.PK_MenuItemId == menuItemId).ExecuteDeleteAsync();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        // Retrieves all menu items from the Db
        public async Task<List<MenuItem>> GetAllMenuItemAsync()
        {
            var menuItems = await _context.MenuItems.ToListAsync();
            return menuItems;
        }


        // Retrieves a single menu item by Id. Returns null (FirstOrDefault) if the item does not exist
        public async Task<MenuItem?> GetMenuItemByIdAsync(int menuItemId)
        {
            var menuItem = await _context.MenuItems
                .FirstOrDefaultAsync(m => m.PK_MenuItemId == menuItemId);
            return menuItem;
        }

        // Retrieve a List of all menu items that belong to a specific category
        public async Task<List<MenuItem>> GetMenuItemByCategoryAsync(string category)
        {
            var menuItems = await _context.MenuItems
                // Case insensitive comparison for category
                .Where(m => m.Category != null && m.Category.ToLower() == category.ToLower())
                .ToListAsync();
            return menuItems;
        }

        // Updates an existing menu item in the Db. Returns true if a row was affected, false if no changes were made
        public async Task<bool> UpdateMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
            var result = await _context.SaveChangesAsync();
            if (result != 0)
            {
                return true;
            }
            return false;
        }
    }
}