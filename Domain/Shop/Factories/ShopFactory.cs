using Domain.Shop.ValueObjects;
using Domain.Shared.ValueObjects;

namespace Domain.Shop.Factories
{
    public sealed class ShopFactory : IShopFactory
    {
        public Shop Create(ShopId id,
                                   Email email,
                                   PasswordHash passwordHash,
                                   Name ownerName,
                                   LastName ownerLastName,
                                   ShopName shopName,
                                   Address shopAddress,
                                   TaxNumber taxNumber,
                                   TelephoneNumber contactNumber) =>
            new(id, email, passwordHash, ownerName, ownerLastName, shopName, shopAddress, taxNumber, contactNumber);
    }
}
