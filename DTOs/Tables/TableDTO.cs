namespace BrewAPI.DTOs.Tables
{
    public class TableDTO
    {
        public int TableId { get; set; } // Mapp PK_TableId -> TableId
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
    }
}