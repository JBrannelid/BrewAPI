using BrewAPI.DTOs.Customers;
using BrewAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For now: Authorize policy on AdminsOrManagers. Easy to scale up with different role 
// Each function asynchronously handles CRUD operations
// TODO: A better global error and logging handeling

namespace BrewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCustomer(CreateCustomerDTO createCustomerDTO)
        {
            var customerId = await _customerService.CreateCustomerAsync(createCustomerDTO);

            // 201 Created with header 
            return CreatedAtAction(nameof(GetCustomerById), new { id = customerId }, customerId);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult> UpdateCustomer(int id, UpdateCustomerDTO updateCustomerDTO)
        {
            var customer = await _customerService.UpdateCustomerAsync(id, updateCustomerDTO);
            if (!customer)
            {
                // Returning 404 if trying to update a non-existing customer
                return NotFound();
            }
            // 204 - when update succeeds but no body is returned
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerService.DeleteCustomerAsync(id);
            if (!customer)
            {
                // 404 if trying to delete a non-existing customer
                return NotFound();
            }
            // 204 No Content on successful deletion
            return NoContent();
        }

        [HttpGet("search")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<List<CustomerSearchDTO>>> SearchCustomers([FromQuery] string searchTerm)
        {
            // Prevents db query when the search term is not meaningful 
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Ok(new List<CustomerSearchDTO>());
            }

            var matchedCustomers = await _customerService.SearchCustomersAsync(searchTerm);
            return Ok(matchedCustomers);
        }
    }
}