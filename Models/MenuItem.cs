namespace BrewAPI.Models
{
    public class MenuItem
    {
        public int PK_MenuItemId { get; set; }
        public string Name { get; set; }
        public string? Category { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsPopular { get; set; } = false;
        public string? ImageUrl { get; set; }
    }
}

// MenuItem entity: stores information about a menu item including and optional metadata (Description, Caregory, img, flag)
// Data validation and constraints are handled via Fluent API in DbContext