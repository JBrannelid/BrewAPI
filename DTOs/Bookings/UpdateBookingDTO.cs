namespace BrewAPI.DTOs.Bookings
{
    public class UpdateBookingDTO
    {
        public int CustomerId { get; set; }
        public int NumberGuests { get; set; }
        public string Status { get; set; } = string.Empty;
        public int TableId { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeOnly BookingTime { get; set; }
        public TimeSpan DurationTime { get; set; }
    }
}
