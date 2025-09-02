using BrewAPI.DTOs.Tables;
using BrewAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/Tables
        // Only a Admin or Manager should be able to se information about the Café layout
        [HttpGet]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<List<TableDTO>>> GetAllTables()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables);
        }

        // GET: api/Tables/{id}
        // Only a Admin or Manager should be able to se information about the Café layout
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

        // GET: api/Tables/number/{tableNumber}
        // Only a Admin or Manager should be able to se information about the Café layout
        [HttpGet("number/{tableNumber}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<TableDTO>> GetTableByTableNumber(int tableNumber)
        {
            var table = await _tableService.GetTableByTableNumberAsync(tableNumber);
            if (table == null)
            {
                return NotFound();
            }
            return Ok(table);
        }

        // POST: api/Tables
        // Only a Admin or Manager should be able to create a new table in the system
        [HttpPost]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<int>> CreateTable(TableDTO tableDto)
        {
            var tableId = await _tableService.CreateTableAsync(tableDto);

            // Return 201 Created with location header
            return CreatedAtAction(nameof(GetTableById), new { id = tableId }, tableId);
        }

        // PUT: api/Tables/{id}
        // Only a Admin or Manager should be able to update a table in the system
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult> UpdateTable(int id, TableDTO tableDto)
        {
            var result = await _tableService.UpdateTableAsync(id, tableDto);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Tables/{id}
        // Only a Admin or Manager should be able to delete a table in the system
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