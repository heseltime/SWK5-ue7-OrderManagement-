using OrderManagement.Domain;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace OrderManagement.Api.Dtos
{
    public record CustomerDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required int ZipCode { get; set; }

        public required string City { get; set; }

        public required Rating Rating { get; set; }

        public decimal TotalRevenue { get; set; }
    }
}
