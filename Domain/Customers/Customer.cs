using Domain.Customers.ValueObjects;
using Domain.Shared.ValueObjects;

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

        public Roles Role { get; private set; } = Roles.customer;
                
        //private Customer() { }
        internal Customer(CustomerId id,
                          Email email,
                          PasswordHash passwordHash,
                          Name name,
                          LastName lastName,
                          Address address,
                          TelephoneNumber telephoneNumber)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            Name = name;
            LastName = lastName;
            Address = address;
            TelephoneNumber = telephoneNumber;
            Role = Roles.customer;
        }
    }
}
