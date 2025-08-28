using BrewAPI.Models;

namespace BrewAPI.Repositories.IRepositories
{
    public interface IMenuItemRepository
    {
        Task<List<MenuItem>> GetAllMenuItemAsync();

        Task<MenuItem?> GetMenuItemByIdAsync(int menuItemId);

        Task<List<MenuItem>> GetMenuItemByCategoryAsync(string category);

        Task<int> CreateMenuItemAsync(MenuItem menuItem);

        Task<bool> UpdateMenuItemAsync(MenuItem menuItem);

        Task<bool> DeleteMenuItemAsync(int menuItemId);
    }
}