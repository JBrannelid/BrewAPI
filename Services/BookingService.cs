using BrewAPI.DTOs;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using BrewAPI.Services.IServices;
using BrewAPI.Settings;

namespace BrewAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITableRepository _tableRepository;
        private readonly ICustomerRepository _customerRepository;

        public BookingService(IBookingRepository bookingRepository, ITableRepository tableRepository, ICustomerRepository customerRepository)
        {
            _bookingRepository = bookingRepository;
            _tableRepository = tableRepository;
            _customerRepository = customerRepository;
        }

        // Retrieves all bookings and includes related Customer and Table data
        public async Task<List<BookingDTO>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync();
            var bookingDTOs = bookings.Select(b => new BookingDTO
            {
                BookingId = b.PK_BookingId,
                CustomerId = b.FK_CustomerId,
                TableId = b.FK_TableId,
                BookingDate = b.BookingDate,
                BookingTime = b.BookingTime,
                NumberGuests = b.NumberGuests,
                Status = b.Status ?? string.Empty,
                DurationTime = b.DurationTime,
                Customer = b.Customer != null ? new CustomerDTO
                {
                    CustomerId = b.Customer.PK_CustomerId,
                    Name = b.Customer.Name,
                    PhoneNumber = b.Customer.PhoneNumber,
                    Email = b.Customer.Email
                } : null,
                Table = b.Table != null ? new TableDTO
                {
                    TableId = b.Table.PK_TableId,
                    TableNumber = b.Table.TableNumber,
                    Capacity = b.Table.Capacity,
                    IsAvailable = b.Table.IsAvailable
                } : null
            }).ToList();

            // Returns a list of bookings with related customer and table info
            return bookingDTOs; 
        }

        // Retrieves a single booking by Id or null if not found
        public async Task<BookingDTO?> GetBookingByIdAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (booking == null)
            {
                return null;
            }

            var bookingDTO = new BookingDTO
            {
                BookingId = booking.PK_BookingId,
                CustomerId = booking.FK_CustomerId,
                TableId = booking.FK_TableId,
                BookingDate = booking.BookingDate,
                BookingTime = booking.BookingTime,
                NumberGuests = booking.NumberGuests,
                Status = booking.Status ?? string.Empty,
                DurationTime = booking.DurationTime,
                Customer = booking.Customer != null ? new CustomerDTO
                {
                    CustomerId = booking.Customer.PK_CustomerId,
                    Name = booking.Customer.Name,
                    PhoneNumber = booking.Customer.PhoneNumber,
                    Email = booking.Customer.Email
                } : null,
                Table = booking.Table != null ? new TableDTO
                {
                    TableId = booking.Table.PK_TableId,
                    TableNumber = booking.Table.TableNumber,
                    Capacity = booking.Table.Capacity,
                    IsAvailable = booking.Table.IsAvailable
                } : null
            };

            return bookingDTO;
        }

        // Retrieves all bookings for a specific customer or empty list if none exist
        public async Task<List<BookingDTO>> GetBookingsByCustomerIdAsync(int customerId)
        {
            var bookings = await _bookingRepository.GetBookingsByCustomerIdAsync(customerId);
            var bookingDTOs = bookings.Select(b => new BookingDTO
            {
                BookingId = b.PK_BookingId,
                CustomerId = b.FK_CustomerId,
                TableId = b.FK_TableId,
                BookingDate = b.BookingDate,
                BookingTime = b.BookingTime,
                NumberGuests = b.NumberGuests,
                Status = b.Status ?? string.Empty,
                DurationTime = b.DurationTime,
                Customer = b.Customer != null ? new CustomerDTO
                {
                    CustomerId = b.Customer.PK_CustomerId,
                    Name = b.Customer.Name,
                    PhoneNumber = b.Customer.PhoneNumber,
                    Email = b.Customer.Email
                } : null,
                Table = b.Table != null ? new TableDTO
                {
                    TableId = b.Table.PK_TableId,
                    TableNumber = b.Table.TableNumber,
                    Capacity = b.Table.Capacity,
                    IsAvailable = b.Table.IsAvailable
                } : null
            }).ToList();

            return bookingDTOs;
        }

        // Retrieves bookings for a specific table and date for availability checks
        public async Task<List<BookingDTO>> GetBookingsByTableIdAndDateAsync(int tableId, DateOnly date)
        {
            var bookings = await _bookingRepository.GetBookingsByTableIdAndDateAsync(tableId, date);
            var bookingDTOs = bookings.Select(b => new BookingDTO
            {
                BookingId = b.PK_BookingId,
                CustomerId = b.FK_CustomerId,
                TableId = b.FK_TableId,
                BookingDate = b.BookingDate,
                BookingTime = b.BookingTime,
                NumberGuests = b.NumberGuests,
                Status = b.Status ?? string.Empty,
                DurationTime = b.DurationTime,
                Customer = b.Customer != null ? new CustomerDTO
                {
                    CustomerId = b.Customer.PK_CustomerId,
                    Name = b.Customer.Name,
                    PhoneNumber = b.Customer.PhoneNumber,
                    Email = b.Customer.Email
                } : null,
                Table = b.Table != null ? new TableDTO
                {
                    TableId = b.Table.PK_TableId,
                    TableNumber = b.Table.TableNumber,
                    Capacity = b.Table.Capacity,
                    IsAvailable = b.Table.IsAvailable
                } : null
            }).ToList();

            return bookingDTOs;
        }

        // Creates a new booking with default duration after validating table availability
        public async Task<int?> CreateBookingAsync(CreateBookingDto createBookingDto)
        {
            // Validate table availability before creation to prevent race conditions
            var availableTables = await GetAvailableTablesAsync(new AvailableTablesRequestDto
            {
                BookingDate = createBookingDto.BookingDate,
                BookingTime = createBookingDto.BookingTime,
                NumberGuests = createBookingDto.NumberGuests
            });

            // Return null if table is not available
            if (!availableTables.Any(t => t.TableId == createBookingDto.TableId))
            {
                return null;
            }

            var booking = new Booking
            {
                FK_CustomerId = createBookingDto.CustomerId,
                FK_TableId = createBookingDto.TableId,
                BookingDate = createBookingDto.BookingDate,
                BookingTime = createBookingDto.BookingTime,
                NumberGuests = createBookingDto.NumberGuests,
                Status = createBookingDto.Status,
                DurationTime = TimeSpan.FromHours(BookingSettings.DefaultBookingDurationHours)
            };

            return await _bookingRepository.CreateBookingAsync(booking);
        }

        // Updates an existing booking or returns false if not found
        public async Task<bool> UpdateBookingAsync(int bookingId, UpdateBookingDto updateBookingDto)
        {
            var existingBooking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (existingBooking == null)
            {
                return false;
            }

            existingBooking.FK_CustomerId = updateBookingDto.CustomerId;
            existingBooking.FK_TableId = updateBookingDto.TableId;
            existingBooking.BookingDate = updateBookingDto.BookingDate;
            existingBooking.BookingTime = updateBookingDto.BookingTime;
            existingBooking.NumberGuests = updateBookingDto.NumberGuests;
            existingBooking.Status = updateBookingDto.Status;
            existingBooking.DurationTime = updateBookingDto.DurationTime;

            return await _bookingRepository.UpdateBookingAsync(existingBooking);
        }

        // Deletes a booking by Id
        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            return await _bookingRepository.DeleteBookingAsync(bookingId);
        }

        // Returns list of available tables for a given date, time, and number of guests
        // Checks capacity, availability flag, and existing bookings to avoid conflicts
        public async Task<List<TableDTO>> GetAvailableTablesAsync(AvailableTablesRequestDto request)
        {
            var allTables = await _tableRepository.GetAllTableAsync();
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
                    var blockStart = bookingStart.AddHours(-BookingSettings.DefaultBookingDurationHours);
                    var blockEnd = bookingStart.AddHours(BookingSettings.DefaultBookingDurationHours);

                    // Return true if requested time does not overlap with existing booking block
                    return request.BookingTime < blockStart || request.BookingTime >= blockEnd;
                });

                if (isAvailable)
                {
                    availableTables.Add(new TableDTO
                    {
                        TableId = table.PK_TableId,
                        TableNumber = table.TableNumber,
                        Capacity = table.Capacity,
                        IsAvailable = table.IsAvailable
                    });
                }
            }

            // Returns a list of tables that meet the requested criteria
            return availableTables;
        }
    }
}