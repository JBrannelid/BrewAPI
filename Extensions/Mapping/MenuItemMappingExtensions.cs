using BrewAPI.DTOs.MenuItems;
using BrewAPI.Models;

namespace BrewAPI.Extensions.Mapping
{
    public static class MenuItemMappingExtensions
    {
        public static MenuItemDTO MapToMenuItemDto(this MenuItem entity)
        {
            return new MenuItemDTO
            {
                MenuItemId = entity.Id,
                Name = entity.Name,
                Category = entity.Category,
                Price = entity.Price,
                Description = entity.Description,
                IsPopular = entity.IsPopular,
                ImageUrl = entity.ImageUrl
            };
        }

        public static PopularMenuItemDTO MapToPopularMenuItemDto(this MenuItem entity)
        {
            return new PopularMenuItemDTO
            {
                Description = entity.Description,
                ImageUrl = entity.ImageUrl
            };
        }

        public static MenuItem MapToMenuItem(this CreateMenuItemDTO dto)
        {
            return new MenuItem
            {
                Name = dto.Name,
                Category = dto.Category,
                Price = dto.Price,
                Description = dto.Description,
                IsPopular = dto.IsPopular,
                ImageUrl = dto.ImageUrl
            };
        }

        public static void MapToMenuItem(this UpdateMenuItemDTO dto, MenuItem entity)
        {
            entity.Name = dto.Name;
            entity.Category = dto.Category;
            entity.Price = dto.Price;
            entity.Description = dto.Description;
            entity.IsPopular = dto.IsPopular;
            entity.ImageUrl = dto.ImageUrl;
        }
    }
}