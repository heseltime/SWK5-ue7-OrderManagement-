using OrderManagement.Domain;
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Api.Dtos
{
    public class CustomerForUpdateDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }
        [Range(1000, 9999, ErrorMessage = "Zipcide must have 4 digits")]
        public required int ZipCode { get; set; }
        [MaxLength(100)]
        public required string City { get; set; }

        public required Rating Rating { get; set; }
    }
}
