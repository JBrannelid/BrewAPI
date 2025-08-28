using BrewAPI.DTOs;
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
        public async Task<List<TableDTO>> GetAllTablesAsync()
        {
            var tables = await _tableRepository.GetAllTableAsync();
            var tableDTOs = tables.Select(t => new TableDTO
            {
                TableId = t.PK_TableId,
                TableNumber = t.TableNumber,
                Capacity = t.Capacity,
                IsAvailable = t.IsAvailable
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
                IsAvailable = table.IsAvailable
            };

            return tableDTO;
        }

        // Retrieves a table by its table number or returns null if not found
        public async Task<TableDTO?> GetTableByTableNumberAsync(int tableNumber)
        {
            var table = await _tableRepository.GetTableByTableNumberAsync(tableNumber);
            if (table == null)
            {
                return null;
            }

            var tableDTO = new TableDTO
            {
                TableId = table.PK_TableId,
                TableNumber = table.TableNumber,
                Capacity = table.Capacity,
                IsAvailable = table.IsAvailable
            };

            return tableDTO;
        }

        // Creates a new table and returns its Id (Primary key)
        public async Task<int> CreateTableAsync(TableDTO tableDTO)
        {
            var table = new Table
            {
                TableNumber = tableDTO.TableNumber,
                Capacity = tableDTO.Capacity,
                IsAvailable = tableDTO.IsAvailable
            };

            var newTableId = await _tableRepository.CreateTableAsync(table);
            return newTableId;
        }

        // Updates an existing table. Returns true if update succeeded, false if table not found
        public async Task<bool> UpdateTableAsync(int tableId, TableDTO tableDTO)
        {
            var existingTable = await _tableRepository.GetTableByIdAsync(tableId);
            if (existingTable == null)
            {
                return false;
            }

            existingTable.TableNumber = tableDTO.TableNumber;
            existingTable.Capacity = tableDTO.Capacity;
            existingTable.IsAvailable = tableDTO.IsAvailable;

            return await _tableRepository.UpdateTableAsync(existingTable);
        }

        // Deletes a table by its ID. Returns true if deletion succeeded, false if table not found
        public async Task<bool> DeleteTableAsync(int tableId)
        {
            return await _tableRepository.DeleteTableAsync(tableId);
        }
    }
}