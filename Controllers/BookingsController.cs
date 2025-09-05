using BrewAPI.DTOs.Bookings;
using BrewAPI.DTOs.Tables;
using BrewAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For now: Authorize policy on AdminsOrManagers. Easy to scale up with different role 
// Each function asynchronously handles CRUD operations
// TODO: A better global error and logging handeling
// TODO: Unsure of API calls. NOT testet with MVC core (Frontend) yet.

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
                // Returning 404 (when booking not found)
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
            var result = await _bookingService.UpdateBookingAsync(id, updateBookingDTO);
            if (!result)
            {
                // Returning 404 if trying to update a non-existing booking
                return NotFound();
            }
            // 204 - when update succeeds but no body is returned
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult> DeleteBooking(int id)
        {
            var result = await _bookingService.DeleteBookingAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("available-tables")]
        public async Task<ActionResult<List<TableDTO>>> GetAvailableTables(AvailableTablesRequestDTO request)
        {
            // flow: first check → then book.
            var availableTables = await _bookingService.GetAvailableTablesAsync(request);
            return Ok(availableTables);
        }
    }
}