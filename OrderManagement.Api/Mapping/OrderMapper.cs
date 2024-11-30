using OrderManagement.Api.Dtos;

namespace OrderManagement.Api.Mapping
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Domain.Order order)
        {
            return new OrderDto()
            {
                Id = order.Id,
                Article = order.Article,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice
            };
        }
    }
}
