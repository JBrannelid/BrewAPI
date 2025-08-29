using BrewAPI.DTOs;
using BrewAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        // GET: api/Bookings
        // Restricted policy to Admin or Manager to protect customer privacy and sensitive data
        [HttpGet]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<List<BookingDTO>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        // GET: api/Bookings/{id}
        // Restricted policy to Admin or Manager to protect customer privacy and sensitive data
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

        // GET: api/Bookings/customer/{customerId}
        // For admin or manager to see booking history of a specific customer
        [HttpGet("customer/{customerId}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<List<BookingDTO>>> GetBookingsByCustomerId(int customerId)
        {
            var bookings = await _bookingService.GetBookingsByCustomerIdAsync(customerId);
            return Ok(bookings);
        }

        // GET: api/Bookings/table/{tableId}/date/{date}
        // For admin or manager to check table availability or view reservations for a specific date
        [HttpGet("table/{tableId}/date/{date}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<List<BookingDTO>>> GetBookingsByTableIdAndDate(int tableId, DateOnly date)
        {
            var bookings = await _bookingService.GetBookingsByTableIdAndDateAsync(tableId, date);
            return Ok(bookings);
        }

        // POST: api/Bookings
        // Open to public since they must be able to book a table
        [HttpPost]
        public async Task<ActionResult<int>> CreateBooking(CreateBookingDto createBookingDto)
        {
            var bookingId = await _bookingService.CreateBookingAsync(createBookingDto);

            if (bookingId == null)
            {
                return Conflict(new { message = "The table is not available at the selected time." });
            }

            return CreatedAtAction(nameof(GetBookingById), new { id = bookingId }, bookingId);
        }

        // PUT: api/Bookings/{id}
        // For admin or manager to update a booking by Id if needed
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult> UpdateBooking(int id, UpdateBookingDto updateBookingDto)
        {
            var result = await _bookingService.UpdateBookingAsync(id, updateBookingDto);
            if (!result)
            {
                // Returning 404 if trying to update a non-existing booking
                return NotFound();
            }
            // 204 - when update succeeds but no body is returned
            return NoContent();
        }

        // DELETE: api/Bookings/{id}
        // For admin or manager to prevent unauthorized deletion
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

        // POST: api/Bookings/available-tables
        // Allows customers to check availability before creating a booking.
        [HttpPost("available-tables")]
        public async Task<ActionResult<List<TableDTO>>> GetAvailableTables(AvailableTablesRequestDto request)
        {
            // flow: first check → then book.
            var availableTables = await _bookingService.GetAvailableTablesAsync(request);
            return Ok(availableTables);
        }
    }
}