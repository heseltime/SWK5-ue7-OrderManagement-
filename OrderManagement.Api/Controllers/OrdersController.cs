using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Dtos;
using OrderManagement.Api.Mapping;
using OrderManagement.Logic;

namespace OrderManagement.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManagementLogic logic;

        public OrdersController(IOrderManagementLogic orderManagementLogic)
        {
            this.logic = orderManagementLogic 
                ?? throw new ArgumentNullException(nameof(orderManagementLogic));
        }

        // GetOrdersOfCustomer
        // GET /api/customers/<CustomerId>/orders
        [HttpGet("customers/{customerId}/orders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersOfCustomer(Guid customerId)
        {
            if (!await logic.CustomerExistsAsync(customerId))
            {
                return NotFound();
            }

            var ordersOfCustomer = await logic.GetOrdersOfCustomerAsync(customerId);
            return Ok(ordersOfCustomer.Select(o => o.ToOrderDto()));
        }
    }
}
