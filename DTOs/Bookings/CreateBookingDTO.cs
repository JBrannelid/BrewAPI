namespace BrewAPI.DTOs.Bookings
{
    public class CreateBookingDTO
    {
        public int CustomerId { get; set; }
        public int NumberGuests { get; set; }
        public string Status { get; set; } = "Confirmed";
        public int TableId { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeOnly BookingTime { get; set; }
        public TimeSpan DurationTime { get; set; }
    }
}
