using BrewAPI.DTOs.MenuItems;

namespace BrewAPI.Services.IServices
{
    public interface IMenuItemService
    {
        Task<List<MenuItemDTO>> GetAllMenuItemsAsync();

        Task<MenuItemDTO?> GetMenuItemByIdAsync(int menuItemId);

        Task<List<PopularMenuItemDTO>> GetPopularMenuItemsAsync();

        Task<int> CreateMenuItemAsync(CreateMenuItemDTO createMenuItemDto);

        Task<bool> UpdateMenuItemAsync(int menuItemId, UpdateMenuItemDTO updateMenuItemDto);

        Task<bool> DeleteMenuItemAsync(int menuItemId);
    }
}