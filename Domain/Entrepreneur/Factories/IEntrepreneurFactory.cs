using Domain.Entrepreneur.ValueObjects;
using Domain.Shared.ValueObjects;

namespace Domain.Entrepreneur.Factories
{
    public interface IEntrepreneurFactory
    {
        Entrepreneur Create(EntrepreneurId id,
                                   Email email,
                                   PasswordHash passwordHash,
                                   Name name,
                                   LastName lastName,
                                   ShopName shopName,
                                   Address shopAddress,
                                   TaxNumber taxNumber,
                                   TelephoneNumber telephoneNumber);
    }
}