using BrewAPI.Models;
using BrewAPI.DTOs.Customers;

namespace BrewAPI.Repositories.IRepositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<List<CustomerSearchDTO>> SearchAsync(string searchTerm);
    }
}
