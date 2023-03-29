using Domain.Shops;
using Domain.Shared.ValueObjects;


namespace UnitTest.Domain.Shops
{
    public class ShopDomainTest
    {
        
        public ShopDomainTest()
        {
            
        }

        [Fact]
        public void ShowShopAddress_ReturnsShopAddressString()
        {
            // act
            var shop = GetShop();

            // arrange
            var shopAddress = shop.ShowShopAddress();

            // assert
            Assert.NotNull(shopAddress);
            Assert.IsType<string>(shopAddress);
            Assert.Equal("country, city, street, postalCode", shopAddress);
        }

        [Fact]
        public void UpdateShopMethod_SuccessfullyUpdatesShopDetails()
        {
            // act
            var shop = GetShop();
            
            // arrange
            shop.UpdateShopDetails("Updated OwnerName",
                                   "Updated OwnerLastName",
                                   null,
                                   null,
                                   null,
                                   null,
                                   null,
                                   null,
                                   null);

            // assert
            Assert.NotNull(shop);
            Assert.Equal("Updated OwnerName", shop.OwnerName);
            Assert.Equal("Updated OwnerLastName", shop.OwnerLastName);
            Assert.IsType<Shop>(shop);
        }

        private static Shop GetShop()
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
