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
        public Email Email { get; private set; }
        public PasswordHash PasswordHash { get; private set; }
        public Name Name { get; private set; }
        public LastName LastName { get; private set; }
        public TelephoneNumber TelephoneNumber { get; private set; }
        public Address Address { get; private set; }

        public Roles Role { get; private set; } = Roles.user;

        public static Customer CreateRegistered(string email, string passwordHash, string name, string lastName, string country, string city, string street, string postalCode, string telephoneNumber)
        {
            //TODO:
            //check logic, passwordhash, create entity, add to db, push event
            
            var address = Address.CreateAddress(country, city, street, postalCode);
            return new Customer(email, passwordHash, name, lastName, address, telephoneNumber);
        }
        private Customer() { }
        internal Customer(Email email, PasswordHash passwordHash, Name name, LastName lastName, Address address, TelephoneNumber telephoneNumber)
        {
            Id = new Guid();
            Email = email;
            PasswordHash = passwordHash;
            Name = name;
            LastName = lastName;
            Address = address;
            TelephoneNumber = telephoneNumber;
        }
    }
}
