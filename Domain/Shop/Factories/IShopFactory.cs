using Domain.Shop.ValueObjects;
using Domain.Shared.ValueObjects;

namespace Domain.Shop.Factories
{
    public interface IShopFactory
    {
        Shop Create(ShopId id,
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