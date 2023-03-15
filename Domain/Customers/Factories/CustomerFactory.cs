using Domain.Customers.ValueObject;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Factories
{
    public sealed class CustomerFactory : ICustomerFactory
    {
        public Customer Create(CustomerId id,
                               Email email,
                               PasswordHash passwordHash,
                               Name name,
                               LastName lastName,
                               Address address,
                               TelephoneNumber telephoneNumber) =>
            new(id, email, passwordHash, name, lastName, address, telephoneNumber);
    }
}
