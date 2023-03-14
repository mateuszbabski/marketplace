using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.ValueObject
{
    public record Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

        public Address(string country, string city, string street, string postalCode)
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
    }
}
