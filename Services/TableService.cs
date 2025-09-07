using BrewAPI.DTOs.Tables;
using BrewAPI.Extensions.Mapping;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using BrewAPI.Services.IServices;

namespace BrewAPI.Services
{
    public class TableService : ITableService
    {
        private readonly IGenericRepository<Table> _tableRepository;

        public TableService(IGenericRepository<Table> tableRepository)
        {
            _tableRepository = tableRepository;
        }

        // Retrieves all tables from the database
        public async Task<List<GetTableDTO>> GetAllTablesAsync()
        {
            var tables = await _tableRepository.GetAllAsync();
            return tables.Select(t => t.MapToGetTableDto()).ToList();
        }

        // Retrieves a table by its ID or returns null if not found
        public async Task<TableDTO?> GetTableByIdAsync(int tableId)
        {
            var table = await _tableRepository.GetByIdAsync(tableId);
            return table?.MapToTableDto();
        }

        // Creates a new table and returns its Id (Primary key)
        public async Task<int> CreateTableAsync(CreateTableDTO createTableDTO)
        {
            var table = createTableDTO.MapToTable();
            var createdTable = await _tableRepository.CreateAsync(table);
            return createdTable.PK_TableId;
        }

        // Updates an existing table. Returns true if update succeeded, false if table not found
        public async Task<bool> UpdateTableAsync(int tableId, UpdateTableDTO updateTableDTO)
        {
            var existingTable = await _tableRepository.GetByIdAsync(tableId);
            if (existingTable == null)
            {
                return false;
            }

            updateTableDTO.MapToTable(existingTable);
            await _tableRepository.UpdateAsync(existingTable);
            return true;
        }

        // Deletes a table by its ID. Returns true if deletion succeeded, false if table not found
        public async Task<bool> DeleteTableAsync(int tableId)
        {
            return await _tableRepository.DeleteAsync(tableId);
        }
    }
}