using BrewAPI.DTOs.Bookings;
using BrewAPI.DTOs.Customers;
using BrewAPI.DTOs.Tables;
using BrewAPI.Models;
using BrewAPI.Settings;

namespace BrewAPI.Extensions.Mapping
{
    public static class BookingMappingExtensions
    {
        public static BookingDTO MapToBookingDto(this Booking entity)
        {
            return new BookingDTO
            {
                BookingId = entity.PK_BookingId,
                CustomerId = entity.FK_CustomerId,
                TableId = entity.FK_TableId,
                BookingDate = entity.BookingDate,
                BookingTime = entity.BookingTime,
                NumberGuests = entity.NumberGuests,
                Status = entity.Status ?? string.Empty,
                DurationTime = entity.DurationTime,
                Customer = entity.Customer?.MapToCustomerDto(),
                Table = entity.Table?.MapToTableDto()
            };
        }

        public static Booking MapToBooking(this CreateBookingDTO dto)
        {
            return new Booking
            {
                FK_CustomerId = dto.CustomerId,
                FK_TableId = dto.TableId,
                BookingDate = dto.BookingDate,
                BookingTime = dto.BookingTime,
                NumberGuests = dto.NumberGuests,
                Status = dto.Status,
                DurationTime = TimeSpan.FromHours(BookingSettings.DefaultBookingDurationHours)
            };
        }

        public static void MapToBooking(this UpdateBookingDTO dto, Booking entity)
        {
            entity.FK_CustomerId = dto.CustomerId;
            entity.FK_TableId = dto.TableId;
            entity.BookingDate = dto.BookingDate;
            entity.BookingTime = dto.BookingTime;
            entity.NumberGuests = dto.NumberGuests;
            entity.Status = dto.Status;
            entity.DurationTime = dto.DurationTime;
        }
    }
}