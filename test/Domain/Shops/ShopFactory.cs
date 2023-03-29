using Domain.Shared.ValueObjects;
using Domain.Shops;

namespace UnitTest.Domain.Shops
{
    public class ShopFactory
    {
        public static Shop Create()
        {
            var address = Address.CreateAddress("country", "city", "street", "postalCode");

            var shop = Shop.Create(Guid.NewGuid(),
                                                           "mail@example.com",
                                                           "passwordHash",
                                                           "ownerName",
                                                           "ownerLastName",
                                                           "shopName",
                                                           address,
                                                           "taxNumber",
                                                           "1234567890");

            return shop;
        }
    }
}
