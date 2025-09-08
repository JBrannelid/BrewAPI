namespace BrewAPI.Settings
{
    public class BookingSettings
    {
        // Default booking duration in hours
        public const int DefaultBookingDurationHours = 2;

        // Café opening hours 08:00-21:00
        public static readonly TimeOnly OpeningTime = new(8, 0);   // 08:00
        public static readonly TimeOnly ClosingTime = new(21, 0);  // 21:00
        public static readonly TimeOnly LastBookingTime = ClosingTime.AddHours(-DefaultBookingDurationHours);

        // 30 minut Booking-slots 
        public const int BookingTimeIntervalMinutes = 30;
    }
}