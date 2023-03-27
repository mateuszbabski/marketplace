using Domain.Shared.Exceptions;
using System.Linq;

namespace Domain.Shared.ValueObjects
{
    public record Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

        private Address(string country, string city, string street, string postalCode)
        {
            Country = country;
            City = city;
            Street = street;
            PostalCode = postalCode;
        }

        public static Address CreateAddress(string country, string city, string street, string postalCode)
        {
            return new Address(country, city, street, postalCode);
        }

        public override string ToString()
            => $"{Country}, {City}, {Street}, {PostalCode}";
    }
}
