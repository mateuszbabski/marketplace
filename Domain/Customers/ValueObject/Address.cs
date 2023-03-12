using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.ValueObject
{
    public record Address
    {
        public string Country { get; }
        public string City { get; }
        public string Street { get; }
        public string PostalCode { get; }

        public Address(string country, string city, string street, string postalCode)
        {
            Country = country;
            City = city;
            Street = street;
            PostalCode = postalCode;
        }
    }
}
