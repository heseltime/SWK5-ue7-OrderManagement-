using OrderManagement.Api.Dtos;
using Riok.Mapperly.Abstractions;
using System.Runtime.CompilerServices;

namespace OrderManagement.Api.Mapper
{
    [Mapper]
    public static partial class CustomerMapper
    {
        public static partial CustomerDto ToCustomerDto(this Domain.Customer customer);
        public static partial IEnumerable<CustomerDto> ToCustomerDtoEnumeration(
            this IEnumerable<Domain.Customer> customers);
        [MapperIgnoreTarget(nameof(Domain.Customer.TotalRevenue))]
        public static partial Domain.Customer ToCustomer(
            this CustomerForCreationDto customerForCreationDto);
    }
}
