using BrewAPI.DTOs.Bookings;
using BrewAPI.DTOs.Tables;
using BrewAPI.Extensions.Mapping;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using BrewAPI.Services.IServices;

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
            // Validate table availability before creation to prevent race conditions
            var availableTables = await GetAvailableTablesAsync(new AvailableTablesRequestDTO
            {
                BookingDate = createBookingDTO.BookingDate,
                BookingTime = createBookingDTO.BookingTime,
                NumberGuests = createBookingDTO.NumberGuests
            });

            // Return null if table is not available
            if (!availableTables.Any(t => t.TableId == createBookingDTO.TableId))
            {
                return null;
            }

            var booking = createBookingDTO.MapToBooking();
            var createdBooking = await _bookingRepository.CreateAsync(booking);
            return createdBooking.PK_BookingId;
        }

        public async Task<bool> UpdateBookingAsync(int bookingId, UpdateBookingDTO updateBookingDto)
        {
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
        public async Task<List<TableDTO>> GetAvailableTablesAsync(AvailableTablesRequestDTO request)
        {
            var allTables = await _tableRepository.GetAllAsync();
            var availableTables = new List<TableDTO>();

            foreach (var table in allTables)
            {
                if (table.Capacity < request.NumberGuests || !table.IsAvailable)
                    continue; // Skip tables that are too small or inactive

                var existingBookings = await _bookingRepository.GetBookingsByTableIdAndDateAsync(table.PK_TableId, request.BookingDate);

                // Check if any existing booking conflicts with the requested time
                bool isAvailable = existingBookings.All(booking =>
                {
                    var bookingStart = booking.BookingTime;

                    // A booking blocks the table for a duration before and after the booking time
                    var blockStart = bookingStart.AddHours(-Settings.BookingSettings.DefaultBookingDurationHours);
                    var blockEnd = bookingStart.AddHours(Settings.BookingSettings.DefaultBookingDurationHours);

                    // Return true if requested time does not overlap with existing booking block
                    return request.BookingTime < blockStart || request.BookingTime >= blockEnd;
                });

                if (isAvailable)
                {
                    availableTables.Add(table.MapToTableDto());
                }
            }
            return availableTables;
        }
    }
}