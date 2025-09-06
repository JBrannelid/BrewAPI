using BrewAPI.DTOs.MenuItems;
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
            var menuItem = await _menuItemRepository.GetByIdAsync(menuItemId);
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

        // Retrieves all popular menu items (IsPopular = true)
        public async Task<List<PopularMenuItemDTO>> GetPopularMenuItemsAsync()
        {
            var allMenuItems = await _menuItemRepository.GetAllAsync();
            var popularMenuItems = allMenuItems.Where(m => m.IsPopular).ToList();

            var popularMenuItemDTOs = popularMenuItems.Select(m => new PopularMenuItemDTO
            {
                Description = m.Description,
                ImageUrl = m.ImageUrl
            }).ToList();

            return popularMenuItemDTOs;
        }

        // Creates a new menu item. Returns the primary key (Id) of the created menu item
        public async Task<int> CreateMenuItemAsync(CreateMenuItemDTO createMenuItemDto)
        {
            var menuItem = new MenuItem
            {
                Name = createMenuItemDto.Name,
                Category = createMenuItemDto.Category,
                Price = createMenuItemDto.Price,
                Description = createMenuItemDto.Description,
                IsPopular = createMenuItemDto.IsPopular,
                ImageUrl = createMenuItemDto.ImageUrl
            };

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

            existingMenuItem.Name = updateMenuItemDto.Name;
            existingMenuItem.Category = updateMenuItemDto.Category;
            existingMenuItem.Price = updateMenuItemDto.Price;
            existingMenuItem.Description = updateMenuItemDto.Description;
            existingMenuItem.IsPopular = updateMenuItemDto.IsPopular;
            existingMenuItem.ImageUrl = updateMenuItemDto.ImageUrl;

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