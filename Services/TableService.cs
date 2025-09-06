using BrewAPI.DTOs.Tables;
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
            var tableDTOs = tables.Select(t => new GetTableDTO
            {
                TableId = t.PK_TableId,
                TableNumber = t.TableNumber,
                Capacity = t.Capacity,
            }).ToList();

            return tableDTOs;
        }

        // Retrieves a table by its ID or returns null if not found
        public async Task<TableDTO?> GetTableByIdAsync(int tableId)
        {
            var table = await _tableRepository.GetByIdAsync(tableId);
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
        public async Task<int> CreateTableAsync(CreateTableDTO createTableDTO)
        {
            var table = new Table
            {
                TableNumber = createTableDTO.TableNumber,
                Capacity = createTableDTO.Capacity,
                IsAvailable = true 
            };

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

            existingTable.TableNumber = updateTableDTO.TableNumber;
            existingTable.Capacity = updateTableDTO.Capacity;
            existingTable.IsAvailable = updateTableDTO.IsAvailable;

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