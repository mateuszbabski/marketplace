using Domain.Shop.ValueObjects;
using Domain.Shared.ValueObjects;

namespace Domain.Shop.Factories
{
    public sealed class ShopFactory : IShopFactory
    {
        public Shop Create(ShopId id,
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
