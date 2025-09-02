using BrewAPI.DTOs.MenuItems;
using BrewAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemsController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        // GET: api/MenuItems
        // Public endpoint so all customers can see the menu
        [HttpGet]
        public async Task<ActionResult<List<MenuItemDTO>>> GetAllMenuItems()
        {
            var menuItems = await _menuItemService.GetAllMenuItemsAsync();
            return Ok(menuItems);
        }

        // GET: api/MenuItems/{id}
        // Public endpoint so a customer can recived a specific menu item by Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemDTO>> GetMenuItemById(int id)
        {
            var menuItem = await _menuItemService.GetMenuItemByIdAsync(id);
            if (menuItem == null)
            {
                // Returning 404 if item not found
                return NotFound();
            }
            return Ok(menuItem);
        }

        // GET: api/MenuItems/category/{category}
        // Public endpoints allow filtering menu items by category
        [HttpGet("category/{category}")]
        public async Task<ActionResult<List<MenuItemDTO>>> GetMenuItemsByCategory(string category)
        {
            var menuItems = await _menuItemService.GetMenuItemsByCategoryAsync(category);
            return Ok(menuItems);
        }

        // GET: api/MenuItems/popular
        // Public endpoint to fetch and highlighting popular menu items
        [HttpGet("popular")]
        public async Task<ActionResult<List<MenuItemDTO>>> GetPopularMenuItems()
        {
            var popularMenuItems = await _menuItemService.GetPopularMenuItemsAsync();
            return Ok(popularMenuItems);
        }

        // POST: api/MenuItems
        // Restricted to Admin or Manager since only staff should create menu items
        [HttpPost]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<int>> CreateMenuItem(MenuItemDTO menuItemDTO)
        {
            var menuItemId = await _menuItemService.CreateMenuItemAsync(menuItemDTO);

            // Returns 201 with Location header
            return CreatedAtAction(nameof(GetMenuItemById), new { id = menuItemId }, menuItemId);
        }

        // PUT: api/MenuItems/{id}
        // For Admin or Manager to update a menu item (price, img, description, name ect)
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult> UpdateMenuItem(int id, MenuItemDTO menuItemDTO)
        {
            var result = await _menuItemService.UpdateMenuItemAsync(id, menuItemDTO);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/MenuItems/{id}
        // Restricted for Admin or Manager only 
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult> DeleteMenuItem(int id)
        {
            var result = await _menuItemService.DeleteMenuItemAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}