using Domain.Customers.ValueObject;
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
        public CustomerId Id { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public TelephoneNumber TelephoneNumber { get; private set; }
        public Address Address { get; private set; }

        public Roles Role { get; private set; } = Roles.user;

        public static Customer CreateRegistered(string email, string password, string name, string lastName, Address address, TelephoneNumber telephoneNumber)
        {
            //check logic, hashpassword, create entity, add to db, push event
            //return new Customer(email, hashpassword, name, lastName, address, telephoneNumber);
            throw new NotImplementedException();
        }

        private Customer(string email, string hashpassword, string name, string lastName, Address address, TelephoneNumber telephoneNumber)
        {
            Id = new CustomerId(Guid.NewGuid());
            Email = email;
            PasswordHash = hashpassword;
            Name = name;
            LastName = lastName;
            Address = address;
            TelephoneNumber = telephoneNumber;
        }
    }
}
