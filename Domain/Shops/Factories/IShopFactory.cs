using Domain.Shops.ValueObjects;
using Domain.Shared.ValueObjects;

namespace Domain.Shops.Factories
{
    public interface IShopFactory
    {
        Shop Create(ShopId id,
                                   Email email,
                                   PasswordHash passwordHash,
                                   Name ownerName,
                                   LastName ownerLastName,
                                   ShopName shopName,
                                   Address shopAddress,
                                   TaxNumber taxNumber,
                                   TelephoneNumber contactNumber);
    }
}