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

        public static Customer CreateRegistered(Email email, string password, Name name, LastName lastName, Address address, TelephoneNumber telephoneNumber)
        {
            //check logic, passwordhash, create entity, add to db, push event
            //return new Customer(email, passwordhash, name, lastName, address, telephoneNumber);
            throw new NotImplementedException();
            //return new Customer(email, passwordhash, name, lastName, address, telephoneNumber);
        }

        private Customer(Email email, PasswordHash passwordhash, Name name, LastName lastName, Address address, TelephoneNumber telephoneNumber)
        {
            Id = new CustomerId(Guid.NewGuid());
            Email = email;
            PasswordHash = passwordhash;
            Name = name;
            LastName = lastName;
            Address = address;
            TelephoneNumber = telephoneNumber;
        }
    }
}
