using Domain.Shared.Exceptions;
using Domain.Shared.ValueObjects;
using Domain.Shops;
using Domain.Shops.Entities.Products.Factories;
using Domain.Shops.Factories;
using Domain.Shops.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Domain.Shops
{
    public class ShopFactoryTest
    {

        [Fact]
        public void CreateShop_CreatesValidShopObject()
        {
            var shopFactory = new ShopFactory();
            var address = Address.CreateAddress("country", "city", "street", "postalCode");
            var validShop = shopFactory.Create(Guid.NewGuid(),
                                                           "mail@example.com",
                                                           "passwordHash",
                                                           "ownerName",
                                                           "ownerLastName",
                                                           "shopName",
                                                           address,
                                                           "taxNumber",
                                                           "1234567890");

            Assert.NotNull(validShop);
            Assert.IsType<Shop>(validShop);
            Assert.IsType<Address>(address);
        }

        [Fact]
        public void CreateShop_ThrowsInvalidEmailExceptionWhenEmailAddressIsInvalid()
        {
            var shopFactory = new ShopFactory();

            var address = Address.CreateAddress("country", "city", "street", "postalCode");

            Assert.Throws<InvalidEmailException>(() => shopFactory.Create(Guid.NewGuid(),
                                                           "",
                                                           "passwordHash",
                                                           "ownerName",
                                                           "ownerLastName",
                                                           "shopName",
                                                           address,
                                                           "taxNumber",
                                                           "1234567890"));   
        }

    }
}
