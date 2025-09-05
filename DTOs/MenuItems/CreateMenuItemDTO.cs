using System.ComponentModel.DataAnnotations;

namespace BrewAPI.DTOs.MenuItems
{
    public class CreateMenuItemDTO
    {
        public string Name { get; set; }

        public string? Category { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public bool IsPopular { get; set; } = false;

        public string? ImageUrl { get; set; }
    }
}