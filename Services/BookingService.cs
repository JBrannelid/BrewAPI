using BrewAPI.DTOs.Bookings;
using BrewAPI.DTOs.Tables;
using BrewAPI.Extensions.Mapping;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using BrewAPI.Services.IServices;
using BrewAPI.Settings;
using static BrewAPI.Extensions.Mapping.BookingMappingExtensions;

namespace BrewAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IGenericRepository<Table> _tableRepository;

        public BookingService(IBookingRepository bookingRepository, IGenericRepository<Table> tableRepository)
        {
            _bookingRepository = bookingRepository;
            _tableRepository = tableRepository;
        }

        // Retrieves all bookings and includes related Customer and Table data
        public async Task<List<BookingDTO>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();
            return bookings.Select(b => b.MapToBookingDto()).ToList();
        }

        public async Task<BookingDTO?> GetBookingByIdAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            return booking?.MapToBookingDto();
        }

        public async Task<List<BookingDTO>> GetBookingsByCustomerIdAsync(int customerId)
        {
            var bookings = await _bookingRepository.GetBookingsByCustomerIdAsync(customerId);
            return bookings.Select(b => b.MapToBookingDto()).ToList();
        }

        // Retrieves bookings for a specific table and date for availability checks
        public async Task<List<BookingDTO>> GetBookingsByTableIdAndDateAsync(int tableId, DateOnly date)
        {
            var bookings = await _bookingRepository.GetBookingsByTableIdAndDateAsync(tableId, date);
            return bookings.Select(b => b.MapToBookingDto()).ToList();
        }

        // Creates a new booking with default duration after validating table availability
        public async Task<int?> CreateBookingAsync(CreateBookingDTO createBookingDTO)
        {
            // Validate booking time 
            if (!IsValidBookingTime(createBookingDTO.BookingTime))
            {
                return null; // Invalid time slot
            }

            // Validate table availability before creation to prevent race conditions
            var availableTablesRequest = MapToAvailableTablesRequest(
                createBookingDTO.BookingDate,
                createBookingDTO.BookingTime,
                createBookingDTO.NumberGuests);
            var availableTables = await GetAvailableTablesAsync(availableTablesRequest);

            // Return null if table is not available
            if (!availableTables.Any(t => t.TableId == createBookingDTO.TableId))
            {
                return null;
            }

            var booking = createBookingDTO.MapToBooking();
            var createdBooking = await _bookingRepository.CreateAsync(booking);

            return createdBooking.Id;
        }

        public async Task<bool> UpdateBookingAsync(int bookingId, UpdateBookingDTO updateBookingDto)
        {
            // Validate booking time if it's being changed
            if (!IsValidBookingTime(updateBookingDto.BookingTime))
            {
                return false; 
            }

            var existingBooking = await _bookingRepository.GetByIdAsync(bookingId);
            if (existingBooking == null)
            {
                return false;
            }

            updateBookingDto.MapToBooking(existingBooking);
            await _bookingRepository.UpdateAsync(existingBooking);
            return true;
        }

        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            return await _bookingRepository.DeleteAsync(bookingId);
        }

        // Returns list of available tables for a given date, time, and number of guests
        // Checks capacity, availability flag, and existing bookings to avoid conflicts
        public async Task<List<TableDTO>> GetAvailableTablesAsync(AvailableTablesDTO request)
        {
            var allTables = await _tableRepository.GetAllAsync();
            var availableTables = new List<TableDTO>();

            foreach (var table in allTables)
            {
                if (table.Capacity < request.NumberGuests || !table.IsAvailable)
                    continue; // Skip tables that are too small or inactive

                var existingBookings = await _bookingRepository.GetBookingsByTableIdAndDateAsync(table.Id, request.BookingDate);

                // Check if any existing booking conflicts with the requested time
                bool isAvailable = existingBookings.All(booking =>
                {
                    // Calculate new booking time slot
                    var newBookingStart = request.BookingTime;
                    var newBookingEnd = request.BookingTime.AddHours(BookingSettings.DefaultBookingDurationHours);

                    // Calculate existing booking time slot
                    var existingBookingStart = booking.BookingTime;
                    var existingBookingEnd = booking.BookingTime.AddHours(booking.DurationTime.TotalHours);

                    // Check for time overlap: bookings conflict if they overlap at all
                    bool hasOverlap = newBookingStart < existingBookingEnd && existingBookingStart < newBookingEnd;

                    // Return true if NO overlap 
                    return !hasOverlap;
                });

                if (isAvailable)
                {
                    availableTables.Add(table.MapToTableDto());
                }
            }
            return availableTables;
        }

        // Only allows bookings on timeslot and within opening hours
        public bool IsValidBookingTime(TimeOnly bookingTime)
        {
            // Check if time is within opening hours
            if (bookingTime < BookingSettings.OpeningTime || bookingTime > BookingSettings.LastBookingTime)
                return false;

            // Check if minutes align with booking intervals 
            return bookingTime.Minute % BookingSettings.BookingTimeIntervalMinutes == 0;
        }

        // Gets all available time slots 
        public List<TimeOnly> GetAvailableTimeSlots()
        {
            var timeSlots = new List<TimeOnly>();
            var currentTimeSlot = BookingSettings.OpeningTime;

            while (currentTimeSlot <= BookingSettings.LastBookingTime)
            {
                timeSlots.Add(currentTimeSlot);
                currentTimeSlot = currentTimeSlot.AddMinutes(BookingSettings.BookingTimeIntervalMinutes);
            }

            return timeSlots;
        }

        // Gets available tables for specific date, time and guest count
        public async Task<BookingAvailabilityDTO> GetBookingAvailabilityAsync(DateOnly date, TimeOnly time, int numberOfGuests)
        {
            var availableTablesRequest = MapToAvailableTablesRequest(date, time, numberOfGuests);
            var availableTables = await GetAvailableTablesAsync(availableTablesRequest);

            return MapToBookingAvailabilityDto(date, time, numberOfGuests, availableTables);
        }
    }
}