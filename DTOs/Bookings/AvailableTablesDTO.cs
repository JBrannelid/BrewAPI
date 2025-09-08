namespace BrewAPI.DTOs.Bookings
{
    public class AvailableTablesDTO
    {
        public int NumberGuests { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeOnly BookingTime { get; set; }
    }
}
