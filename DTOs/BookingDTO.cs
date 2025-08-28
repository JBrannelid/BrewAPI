namespace BrewAPI.DTOs
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int NumberGuests { get; set; }
        public string Status { get; set; }
        public int TableId { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeOnly BookingTime { get; set; }
        public TimeSpan DurationTime { get; set; }
        public CustomerDTO? Customer { get; set; }
        public TableDTO? Table { get; set; }
    }

    public class CreateBookingDto
    {
        public int CustomerId { get; set; }
        public int NumberGuests { get; set; }
        public string Status { get; set; } = "Confirmed";
        public int TableId { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeOnly BookingTime { get; set; }
        public TimeSpan DurationTime { get; set; }
    }

    public class UpdateBookingDto
    {
        public int CustomerId { get; set; }
        public int NumberGuests { get; set; }
        public string Status { get; set; } = string.Empty;
        public int TableId { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeOnly BookingTime { get; set; }
        public TimeSpan DurationTime { get; set; }
    }

    public class AvailableTablesRequestDto
    {
        public int NumberGuests { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeOnly BookingTime { get; set; }
    }
}