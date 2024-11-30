using OrderManagement.Api.Dtos;

namespace OrderManagement.Api.Mapping
{
    public static class CustomerMapper
    {
        //public static CustomerDto ToCustomerDto(this Domain.Customer customer)
        //{
        //    return new CustomerDto()
        //    {
        //        Id = customer.Id,
        //        Name = customer.Name,
        //        City = customer.City,
        //        ZipCode = customer.ZipCode,
        //        Rating = customer.Rating,
        //        TotalRevenue = customer.TotalRevenue,
        //    };
        //}

        public static Domain.Customer ToCustomer(this CustomerDto customerDto)
        {
            return new Domain.Customer(
                customerDto.Id,
                customerDto.Name,
                customerDto.ZipCode,
                customerDto.City,
                customerDto.Rating);
        }
    }
}
