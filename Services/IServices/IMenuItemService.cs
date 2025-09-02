using BrewAPI.DTOs.MenuItems;

namespace BrewAPI.Services.IServices
{
    public interface IMenuItemService
    {
        Task<List<MenuItemDTO>> GetAllMenuItemsAsync();

        Task<MenuItemDTO?> GetMenuItemByIdAsync(int menuItemId);

        Task<List<MenuItemDTO>> GetMenuItemsByCategoryAsync(string category);

        Task<List<MenuItemDTO>> GetPopularMenuItemsAsync();

        Task<int> CreateMenuItemAsync(MenuItemDTO menuItemDTO);

        Task<bool> UpdateMenuItemAsync(int menuItemId, MenuItemDTO menuItemDTO);

        Task<bool> DeleteMenuItemAsync(int menuItemId);
    }
}