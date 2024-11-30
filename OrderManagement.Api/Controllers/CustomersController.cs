using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Dtos;
using OrderManagement.Api.Mapping;
using OrderManagement.Domain;
using OrderManagement.Logic;

namespace OrderManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IOrderManagementLogic logic;

        public CustomersController(IOrderManagementLogic orderManagementLogic)
        {
            this.logic = orderManagementLogic ??
                throw new NullReferenceException(nameof(orderManagementLogic));
        }

        // GET /api/Customers
        // GET /api/Customers?rating=A
        [HttpGet]
        public async Task<IEnumerable<CustomerDto>> GetCustomers([FromQuery] Rating? rating)
        {
            if (rating is null)
            {
                return (await logic.GetCustomersAsync()).Select(c => c.ToCustomerDto());
            }
            else
            {
                return (await logic.GetCustomersByRatingAsync(rating.Value))
                    .Select(c => c.ToCustomerDto());
            }
        }

        // GET /api/Customers/<GUID>
        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById([FromRoute] Guid customerId)
        {
            var customer = await logic.GetCustomerAsync(customerId);

            if (customer is null) {
                return NotFound();
            }

            return Ok(customer.ToCustomerDto());
        }

        // POST /api/Customers
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody] CustomerDto customerDto)
        {
            if (customerDto.Id != Guid.Empty &&
                await logic.CustomerExistsAsync(customerDto.Id))
            {
                return Conflict();
            }

            Domain.Customer customer = customerDto.ToCustomer();
            await logic.AddCustomerAsync(customer);

            return CreatedAtAction(actionName: nameof(GetCustomerById),
                routeValues: new { customerId = customer.Id },
                value: customer.ToCustomerDto());
        }


    }
}
