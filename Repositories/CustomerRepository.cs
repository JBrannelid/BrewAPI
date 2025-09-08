using BrewAPI.Data;
using BrewAPI.DTOs.Customers;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BrewAPI.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BrewAPIDbContext context) : base(context)
        {
        }

        // Search customers by name, phone number, or email
        // Uses EF Core LINQ for query searh
        public async Task<List<CustomerSearchDTO>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return [];

            searchTerm = searchTerm.Trim().ToLower();

            return await _dbSet
                .Where(c =>
                    c.Name.ToLower().Contains(searchTerm) ||
                    c.PhoneNumber.Contains(searchTerm) ||
                    c.Email.ToLower().Contains(searchTerm))
                .OrderBy(c => c.Name)
                .Select(c => new CustomerSearchDTO
                {
                    CustomerId = c.Id,
                    Name = c.Name,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email
                })
                .ToListAsync();
        }
    }
}
