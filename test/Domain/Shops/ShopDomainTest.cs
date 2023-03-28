using Domain.Shops.Factories;
using Domain.Shops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Shared.ValueObjects;
using System.Net;


namespace UnitTest.Domain.Shops
{
    public class ShopDomainTest
    {
        private readonly IShopFactory _shopFactory;
        public ShopDomainTest()
        {
            _shopFactory = new ShopFactory();
        }

        [Fact]
        public void ShowShopAddress_ReturnsShopAddressString()
        {
            var shop = GetShop();

            var shopAddress = shop.ShowShopAddress();

            Assert.NotNull(shopAddress);
            Assert.IsType<string>(shopAddress);
            Assert.Equal("country, city, street, postalCode", shopAddress);
        }

        [Fact]
        public void UpdateShopMethod_SuccessfullyUpdatesShopDetails()
        {
            var shop = GetShop();
            
            shop.UpdateShopDetails("Updated OwnerName",
                                   "Updated OwnerLastName",
                                   null,
                                   null,
                                   null,
                                   null,
                                   null,
                                   null,
                                   null);

            Assert.NotNull(shop);
            Assert.Equal("Updated OwnerName", shop.OwnerName);
            Assert.Equal("Updated OwnerLastName", shop.OwnerLastName);
            Assert.IsType<Shop>(shop);
        }

        private Shop GetShop()
        {
            var address = Address.CreateAddress("country", "city", "street", "postalCode");
            var shop = _shopFactory.Create(Guid.NewGuid(),
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
