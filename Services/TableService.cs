using BrewAPI.DTOs.Tables;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using BrewAPI.Services.IServices;

namespace BrewAPI.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;

        public TableService(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        // Retrieves all tables from the database
        public async Task<List<GetTableDTO>> GetAllTablesAsync()
        {
            var tables = await _tableRepository.GetAllTableAsync();
            var tableDTOs = tables.Select(t => new GetTableDTO
            {
                TableId = t.PK_TableId,
                TableNumber = t.TableNumber,
                Capacity = t.Capacity,
                // IsAvailable is not included in the DTO as per current design
                //IsAvailable = t.IsAvailable
            }).ToList();

            return tableDTOs;
        }

        // Retrieves a table by its ID or returns null if not found
        public async Task<TableDTO?> GetTableByIdAsync(int tableId)
        {
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            if (table == null)
            {
                return null;
            }

            var tableDTO = new TableDTO
            {
                TableId = table.PK_TableId,
                TableNumber = table.TableNumber,
                Capacity = table.Capacity,
                // The DTO does not currently include IsAvailable
                //IsAvailable = table.IsAvailable
            };

            return tableDTO;
        }

        // Creates a new table and returns its Id (Primary key)
        public async Task<int> CreateTableAsync(CreateTableDTO createTableDTO)
        {
            var table = new Table
            {
                TableNumber = createTableDTO.TableNumber,
                Capacity = createTableDTO.Capacity,
            };

            var newTableId = await _tableRepository.CreateTableAsync(table);
            return newTableId;
        }

        // Updates an existing table. Returns true if update succeeded, false if table not found
        public async Task<bool> UpdateTableAsync(int tableId, UpdateTableDTO updateTableDTO)
        {
            var existingTable = await _tableRepository.GetTableByIdAsync(tableId);
            if (existingTable == null)
            {
                return false;
            }

            existingTable.TableNumber = updateTableDTO.TableNumber;
            existingTable.Capacity = updateTableDTO.Capacity;
            existingTable.IsAvailable = updateTableDTO.IsAvailable;

            return await _tableRepository.UpdateTableAsync(existingTable);
        }

        // Deletes a table by its ID. Returns true if deletion succeeded, false if table not found
        public async Task<bool> DeleteTableAsync(int tableId)
        {
            return await _tableRepository.DeleteTableAsync(tableId);
        }
    }
}