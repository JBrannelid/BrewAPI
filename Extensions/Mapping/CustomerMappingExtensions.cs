using BrewAPI.DTOs.Customers;
using BrewAPI.Models;

namespace BrewAPI.Extensions.Mapping
{
    public static class CustomerMappingExtensions
    {
        public static CustomerDTO MapToCustomerDto(this Customer entity)
        {
            return new CustomerDTO
            {
                CustomerId = entity.PK_CustomerId,
                Name = entity.Name,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email
            };
        }

        public static Customer MapToCustomer(this CreateCustomerDTO dto)
        {
            return new Customer
            {
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email
            };
        }

        public static void MapToCustomer(this UpdateCustomerDTO dto, Customer entity)
        {
            entity.Name = dto.Name;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.Email = dto.Email;
        }
    }
}