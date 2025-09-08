using BrewAPI.DTOs.Bookings;
using BrewAPI.DTOs.Tables;
using BrewAPI.Services.IServices;
using BrewAPI.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For now: Authorize policy on AdminsOrManagers. Easy to scale up with different role 
// Each function asynchronously handles CRUD operations
// TODO: A better global error and logging handeling

namespace BrewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<List<BookingDTO>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<BookingDTO>> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }


        [HttpGet("customer/{customerId}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<List<BookingDTO>>> GetBookingsByCustomerId(int customerId)
        {
            var bookings = await _bookingService.GetBookingsByCustomerIdAsync(customerId);
            return Ok(bookings);
        }


        [HttpGet("table/{tableId}/date/{date}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<List<BookingDTO>>> GetBookingsByTableIdAndDate(int tableId, DateOnly date)
        {
            var bookings = await _bookingService.GetBookingsByTableIdAndDateAsync(tableId, date);
            return Ok(bookings);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateBooking(CreateBookingDTO createBookingDTO)
        {
            var bookingId = await _bookingService.CreateBookingAsync(createBookingDTO);

            if (bookingId == null)
            {
                return Conflict(new { message = "The table is not available at the selected time." });
            }

            return CreatedAtAction(nameof(GetBookingById), new { id = bookingId }, bookingId);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult> UpdateBooking(int id, UpdateBookingDTO updateBookingDTO)
        {
            var booking = await _bookingService.UpdateBookingAsync(id, updateBookingDTO);
            if (!booking)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult> DeleteBooking(int id)
        {
            var booking = await _bookingService.DeleteBookingAsync(id);
            if (!booking)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("available-tables")]
        public async Task<ActionResult<List<TableDTO>>> GetAvailableTables(AvailableTablesDTO request)
        {
            // Check if the requested booking date/time is available
            var availableTables = await _bookingService.GetAvailableTablesAsync(request);
            return Ok(availableTables);
        }

        [HttpGet("time-slots")]
        [Authorize(Policy = "AdminOrManager")]
        public ActionResult<List<TimeOnly>> GetAvailableTimeSlots()
        {
            // Returns all possible booking time slots within opening hours defined in BookingSettings
            var timeSlots = _bookingService.GetAvailableTimeSlots();
            return Ok(timeSlots);
        }

        [HttpGet("availability")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<BookingAvailabilityDTO>> GetBookingAvailability(
            DateOnly date,
            TimeOnly time,
            int numberOfGuests)
        {
            if (!_bookingService.IsValidBookingTime(time))
            {
                return BadRequest($"Invalid booking time. Bookings are only allowed on {BookingSettings.BookingTimeIntervalMinutes}-minute intervals between {BookingSettings.OpeningTime:HH:mm} and {BookingSettings.ClosingTime:HH:mm}.");
            }

            var availability = await _bookingService.GetBookingAvailabilityAsync(date, time, numberOfGuests);
            return Ok(availability);
        }
    }
}