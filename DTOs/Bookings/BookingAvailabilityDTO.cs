namespace BrewAPI.DTOs.Bookings
{
    public class BookingAvailabilityDTO
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int NumberGuests { get; set; }
        public List<AvailableTableDTO> AvailableTables { get; set; } = new();
    }

}
