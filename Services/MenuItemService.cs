using BrewAPI.DTOs.MenuItems;
using BrewAPI.Extensions.Mapping;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using BrewAPI.Services.IServices;

namespace BrewAPI.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemService(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        // Retrieve all menu items from Db and map to DTOs
        public async Task<List<MenuItemDTO>> GetAllMenuItemsAsync()
        {
            var menuItems = await _menuItemRepository.GetAllAsync();
            return menuItems.Select(m => m.MapToMenuItemDto()).ToList();
        }

        // Retrieve a specific menu item by ID and map to DTO
        public async Task<MenuItemDTO?> GetMenuItemByIdAsync(int menuItemId)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(menuItemId);
            return menuItem?.MapToMenuItemDto();
        }

        // Retrieves all popular menu items (IsPopular = true)
        public async Task<List<PopularMenuItemDTO>> GetPopularMenuItemsAsync()
        {
            var allMenuItems = await _menuItemRepository.GetAllAsync();
            var popularMenuItems = allMenuItems.Where(m => m.IsPopular);
            return popularMenuItems.Select(m => m.MapToPopularMenuItemDto()).ToList();
        }

        // Creates a new menu item. Returns the primary key (Id) of the created menu item
        public async Task<int> CreateMenuItemAsync(CreateMenuItemDTO createMenuItemDto)
        {
            var menuItem = createMenuItemDto.MapToMenuItem();
            var createdMenuItem = await _menuItemRepository.CreateAsync(menuItem);
            return createdMenuItem.PK_MenuItemId;
        }

        // Updates an existing menu item. Returns true if update was successful, false if item not found
        public async Task<bool> UpdateMenuItemAsync(int menuItemId, UpdateMenuItemDTO updateMenuItemDto)
        {
            var existingMenuItem = await _menuItemRepository.GetByIdAsync(menuItemId);
            if (existingMenuItem == null)
            {
                return false;
            }

            updateMenuItemDto.MapToMenuItem(existingMenuItem);
            await _menuItemRepository.UpdateAsync(existingMenuItem);
            return true;
        }

        // Deletes a menu item by ID. Returns true if deletion was successful, false if item not found
        public async Task<bool> DeleteMenuItemAsync(int menuItemId)
        {
            return await _menuItemRepository.DeleteAsync(menuItemId);
        }
    }
}