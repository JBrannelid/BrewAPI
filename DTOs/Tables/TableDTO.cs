namespace BrewAPI.DTOs.Tables
{
    public class TableDTO
    {
        public int TableId { get; set; }  // Needed for internal operations with BookingService 
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; } // For internal availability 
    }
}