using Domain.Customers.ValueObjects;
using Domain.Shared.ValueObjects;

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
