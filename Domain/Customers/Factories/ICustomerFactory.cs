using Domain.Customers.ValueObjects;
using Domain.Shared.ValueObjects;

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
