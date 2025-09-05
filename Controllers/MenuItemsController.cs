using BrewAPI.DTOs.MenuItems;
using BrewAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For now: Authorize policy on AdminsOrManagers. Easy to scale up with different role 
// MenuItemService interface is injected through the constructor 
// Each function asynchronously handles CRUD operations
// TODO: A better global error and logging handeling

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

        [HttpGet]
        public async Task<ActionResult<List<MenuItemDTO>>> GetAllMenuItems()
        {
            var menuItems = await _menuItemService.GetAllMenuItemsAsync();
            return Ok(menuItems);
        }

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

        [HttpGet("popular")]
        public async Task<ActionResult<List<PopularMenuItemDTO>>> GetPopularMenuItems()
        {
            var popularMenuItems = await _menuItemService.GetPopularMenuItemsAsync();
            return Ok(popularMenuItems);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<int>> CreateMenuItem(CreateMenuItemDTO createMenuItemDTO)
        {
            var menuItemId = await _menuItemService.CreateMenuItemAsync(createMenuItemDTO);

            // Returns 201 with Location header
            return CreatedAtAction(nameof(GetMenuItemById), new { id = menuItemId }, menuItemId);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult> UpdateMenuItem(int id, UpdateMenuItemDTO updateMenuItemDTO)
        {
            var result = await _menuItemService.UpdateMenuItemAsync(id, updateMenuItemDTO);
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