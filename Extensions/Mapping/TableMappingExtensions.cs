using BrewAPI.DTOs.Tables;
using BrewAPI.Models;

namespace BrewAPI.Extensions.Mapping
{
    public static class TableMappingExtensions
    {
        public static TableDTO MapToTableDto(this Table entity)
        {
            return new TableDTO
            {
                TableId = entity.PK_TableId,
                TableNumber = entity.TableNumber,
                Capacity = entity.Capacity,
                IsAvailable = entity.IsAvailable
            };
        }

        public static GetTableDTO MapToGetTableDto(this Table entity)
        {
            return new GetTableDTO
            {
                TableId = entity.PK_TableId,
                TableNumber = entity.TableNumber,
                Capacity = entity.Capacity
            };
        }

        public static Table MapToTable(this CreateTableDTO dto)
        {
            return new Table
            {
                TableNumber = dto.TableNumber,
                Capacity = dto.Capacity,
                IsAvailable = true
            };
        }

        public static void MapToTable(this UpdateTableDTO dto, Table entity)
        {
            entity.TableNumber = dto.TableNumber;
            entity.Capacity = dto.Capacity;
        }
    }
}