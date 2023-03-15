using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.ValueObjects
{
    public record Address(string Country, string City, string Street, string PostalCode)
    {
        //public string Country { get; set; }
        //public string City { get; set; }
        //public string Street { get; set; }
        //public string PostalCode { get; set; }

        //public Address(string country, string city, string street, string postalCode)
        //{
        //    Country = country;
        //    City = city;
        //    Street = street;
        //    PostalCode = postalCode;
        //}

        //public static Address CreateAddress(string country, string city, string street, string postalCode)
        //{
        //    return new Address(country, city, street, postalCode);
        //}

        public static Address CreateAddress(string value)
        {
            var splitAddress = value.Split(',');
            return new Address(splitAddress[0], splitAddress[1], splitAddress[2], splitAddress[3]);
            
        }

        public override string ToString()
            => $"{Country},{City},{Street},{PostalCode}";
    }
}
