using Domain.Customers.ValueObject;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Factories
{
    public interface ICustomerFactory
    {
        Customer Create(CustomerId id,
                               Email email,
                               PasswordHash passwordHash,
                               Name name,
                               LastName lastName,
                               Address address,
                               TelephoneNumber telephoneNumber);
    }
}
