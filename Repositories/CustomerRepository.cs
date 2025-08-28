using BrewAPI.Data;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BrewAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BrewAPIDbContext _context;

        public CustomerRepository(BrewAPIDbContext context)
        {
            _context = context;
        }

        // Retrieves all customers from the database
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var customer = await _context.Customers.ToListAsync();
            return customer;
        }

        // Retrieves a single customer by Id
        // Returns null (FirstOrDefault) if the customer does not exist
        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.PK_CustomerId == customerId);
            return customer;
        }

        // Adds a new customer to the database
        // Returns the Id (Primary Key Id) of the created customer
        public async Task<int> AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer.PK_CustomerId;
        }

        // Updates an existing customer in the database
        // Returns true if any row was affected, false if no changes were made
        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            var result = await _context.SaveChangesAsync();

            if (result != 0)
            {
                return true;
            }
            return false;
        }

        // Deletes a customer by Id
        // Returns true if a row was affected, false if the customer did not exist
        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            var rowsAffected = await _context.Customers.Where(c => c.PK_CustomerId == customerId).ExecuteDeleteAsync();

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
    }
}