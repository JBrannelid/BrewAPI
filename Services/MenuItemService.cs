using BrewAPI.DTOs;
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
            var menuItems = await _menuItemRepository.GetAllMenuItemAsync();
            var menuItemDTOs = menuItems.Select(m => new MenuItemDTO
            {
                MenuItemId = m.PK_MenuItemId,
                Name = m.Name,
                Category = m.Category,
                Price = m.Price,
                Description = m.Description,
                IsPopular = m.IsPopular,
                ImageUrl = m.ImageUrl
            }).ToList();

            return menuItemDTOs;
        }

        // Retrieve a specific menu item by ID and map to DTO
        public async Task<MenuItemDTO?> GetMenuItemByIdAsync(int menuItemId)
        {
            var menuItem = await _menuItemRepository.GetMenuItemByIdAsync(menuItemId);
            if (menuItem == null)
            {
                return null;
            }

            var menuItemDTO = new MenuItemDTO
            {
                MenuItemId = menuItem.PK_MenuItemId,
                Name = menuItem.Name,
                Category = menuItem.Category,
                Price = menuItem.Price,
                Description = menuItem.Description,
                IsPopular = menuItem.IsPopular,
                ImageUrl = menuItem.ImageUrl
            };

            return menuItemDTO;
        }

        // Retrieve menu items filtered by category and map to DTOs
        public async Task<List<MenuItemDTO>> GetMenuItemsByCategoryAsync(string category)
        {
            var menuItems = await _menuItemRepository.GetMenuItemByCategoryAsync(category);
            var menuItemDTOs = menuItems.Select(m => new MenuItemDTO
            {
                MenuItemId = m.PK_MenuItemId,
                Name = m.Name,
                Category = m.Category,
                Price = m.Price,
                Description = m.Description,
                IsPopular = m.IsPopular,
                ImageUrl = m.ImageUrl
            }).ToList();

            return menuItemDTOs;
        }

        // Retrieves all popular menu items (IsPopular = true)
        public async Task<List<MenuItemDTO>> GetPopularMenuItemsAsync()
        {
            var allMenuItems = await _menuItemRepository.GetAllMenuItemAsync();
            var popularMenuItems = allMenuItems.Where(m => m.IsPopular).ToList();

            var menuItemDTOs = popularMenuItems.Select(m => new MenuItemDTO
            {
                MenuItemId = m.PK_MenuItemId,
                Name = m.Name,
                Category = m.Category,
                Price = m.Price,
                Description = m.Description,
                IsPopular = m.IsPopular,
                ImageUrl = m.ImageUrl
            }).ToList();

            return menuItemDTOs;
        }

        // Creates a new menu item. Returns the primary key (Id) of the created menu item
        public async Task<int> CreateMenuItemAsync(MenuItemDTO menuItemDto)
        {
            var menuItem = new MenuItem
            {
                Name = menuItemDto.Name,
                Category = menuItemDto.Category,
                Price = menuItemDto.Price,
                Description = menuItemDto.Description,
                IsPopular = menuItemDto.IsPopular,
                ImageUrl = menuItemDto.ImageUrl
            };

            var newMenuItemId = await _menuItemRepository.CreateMenuItemAsync(menuItem);
            return newMenuItemId;
        }

        // Updates an existing menu item. Returns true if update was successful, false if item not found
        public async Task<bool> UpdateMenuItemAsync(int menuItemId, MenuItemDTO menuItemDto)
        {
            var existingMenuItem = await _menuItemRepository.GetMenuItemByIdAsync(menuItemId);
            if (existingMenuItem == null)
            {
                return false;
            }

            existingMenuItem.Name = menuItemDto.Name;
            existingMenuItem.Category = menuItemDto.Category;
            existingMenuItem.Price = menuItemDto.Price;
            existingMenuItem.Description = menuItemDto.Description;
            existingMenuItem.IsPopular = menuItemDto.IsPopular;
            existingMenuItem.ImageUrl = menuItemDto.ImageUrl;

            return await _menuItemRepository.UpdateMenuItemAsync(existingMenuItem);
        }

        // Deletes a menu item by ID. Returns true if deletion was successful, false if item not found
        public async Task<bool> DeleteMenuItemAsync(int menuItemId)
        {
            return await _menuItemRepository.DeleteMenuItemAsync(menuItemId);
        }
    }
}