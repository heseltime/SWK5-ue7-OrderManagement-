using OrderManagement.Domain;

namespace OrderManagement.Api.Dtos
{
    public class CustomerForCreationDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required int ZipCode { get; set; }

        public required string City { get; set; }

        public required Rating Rating { get; set; }

    }
}
