using BrewAPI.DTOs.Tables;
using BrewAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For now: Authorize policy on AdminsOrManagers. Easy to scale up with different role 
// TabelService interface is injected through the constructor 
// Each function asynchronously handles CRUD operations
// TODO: A better global error and logging handeling

namespace BrewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<List<GetTableDTO>>> GetAllTables()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<TableDTO>> GetTableById(int id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null)
            {
                // Return 404 if table doesn’t exist
                return NotFound();
            }
            return Ok(table);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<int>> CreateTable(CreateTableDTO createTableDTO)
        {
            var tableId = await _tableService.CreateTableAsync(createTableDTO);

            // Return 201 Created with location header
            return CreatedAtAction(nameof(GetTableById), new { id = tableId }, tableId);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult> UpdateTable(int id, UpdateTableDTO updateTableDto)
        {
            var result = await _tableService.UpdateTableAsync(id, updateTableDto);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult> DeleteTable(int id)
        {
            var result = await _tableService.DeleteTableAsync(id);
            if (!result)
            {
                // 404 if the table doesn’t exist
                return NotFound();
            }
            // 204 No Content signals successful deletion
            return NoContent();
        }
    }
}