using Domain.Entrepreneur.ValueObjects;
using Domain.Shared.ValueObjects;

namespace Domain.Entrepreneur.Factories
{
    public sealed class EntrepreneurFactory : IEntrepreneurFactory
    {
        public Entrepreneur Create(EntrepreneurId id,
                                   Email email,
                                   PasswordHash passwordHash,
                                   Name name,
                                   LastName lastName,
                                   ShopName shopName,
                                   Address shopAddress,
                                   TaxNumber taxNumber,
                                   TelephoneNumber telephoneNumber) =>
            new(id, email, passwordHash, name, lastName, shopName, shopAddress, taxNumber, telephoneNumber);
    }
}
