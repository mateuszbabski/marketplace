using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public TelephoneNumber TelephoneNumber { get; private set; }
        public Address Address { get; private set; }

        public static Customer CreateRegistered(string email, string password, Address address, string name, string lastName, TelephoneNumber telephoneNumber)
        {
            throw new NotImplementedException();
        }

        private Customer(string email, string password, Address address, string name, string lastName, TelephoneNumber telephoneNumber)
        {
            Id = Guid.NewGuid();
            Email = email;
            PasswordHash = password;
            //to_implement_hashing
            Name = name;
            LastName = lastName;
            TelephoneNumber = telephoneNumber;
            Address = address;
        }
    }
}
