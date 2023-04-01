using Domain.Shops;
using Domain.Shared.ValueObjects;
using Domain.Shops.ValueObjects;
using Domain.Shared.Exceptions;
using Moq;
using Domain.Shared.Abstractions;
using System.Net;
using System.ComponentModel.DataAnnotations;
using Domain.Shops.Entities.Products;

namespace UnitTest.Domain.Shops
{
    public class ShopDomainTest
    {        

        [Fact] 
        public void CreateShop_ReturnsShopIfParamsAreValid()
        {
            var shopAddress = Address.CreateAddress("country", "city", "street", "postalCode");


            var shop = Shop.Create(Guid.NewGuid(),
                                   "test@example.com",
                                   "passwordHash",
                                   "ownerName",
                                   "ownerLastName",
                                   "shopName",
                                   shopAddress,
                                   "taxNumber",
                                   "contactNumber");

            Assert.NotNull(shop);
            Assert.IsType<Shop>(shop);
            Assert.IsType<ShopId>(shop.Id);
            Assert.Equal("test@example.com", shop.Email);
        }

        [Fact]
        public void CreateShop_ThrowsWhenEmailParamIsInvalid()
        {
            var shopAddress = Address.CreateAddress("country", "city", "street", "postalCode");

            var act = Assert.Throws<InvalidEmailException>(() => Shop.Create(Guid.NewGuid(),
                                   "",
                                   "passwordHash",
                                   "ownerName",
                                   "ownerLastName",
                                   "shopName",
                                   shopAddress,
                                   "taxNumber",
                                   "contactNumber"));

            Assert.IsType<InvalidEmailException>(act);          
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
        public void AddProductToShop_ReturnsProductIfSuccessfull()
        {
            var shop = GetShop();
            var productPrice = MoneyValue.Of(10, "USD");

            var product = shop.AddProduct(Guid.NewGuid(),
                                          "productName",
                                          "productDescription",
                                          productPrice,
                                          "productUnit",
                                          shop.Id);

            Assert.NotNull(product);
            Assert.IsType<Product>(product);
            Assert.Equal("productName", product.ProductName);
        }

        [Fact]
        public void ChangeProductPrice_ChangesPriceIsParamsAreValid()
        {
            var shop = GetShop();
            var productPrice = MoneyValue.Of(10, "USD");

            var product = shop.AddProduct(Guid.NewGuid(),
                                          "productName",
                                          "productDescription",
                                          productPrice,
                                          "productUnit",
                                          shop.Id);

            shop.ChangeProductPrice(product.Id, 20, "USD");

            Assert.NotNull(product);
            Assert.Equal(20, product.Price.Amount);
            Assert.Equal("USD", product.Price.Currency);
        }

        [Fact]
        public void ChangeProductDetails_ChangesDetailsIfParamsAreValid()
        {
            var shop = GetShop();
            var productPrice = MoneyValue.Of(10, "USD");

            var product = shop.AddProduct(Guid.NewGuid(),
                                          "productName",
                                          "productDescription",
                                          productPrice,
                                          "productUnit",
                                          shop.Id);

            shop.ChangeProductDetails(product.Id, "changedName", "changedDescription", "changedUnit");

            Assert.Equal("changedName", product.ProductName);
            Assert.Equal("changedDescription", product.ProductDescription);
            Assert.Equal("changedUnit", product.Unit);
        }

        [Fact]
        public void ChangeProductAvailability_ChangesProductsFlag()
        {
            var shop = GetShop();
            var productPrice = MoneyValue.Of(10, "USD");

            var product = shop.AddProduct(Guid.NewGuid(),
                                          "productName",
                                          "productDescription",
                                          productPrice,
                                          "productUnit",
                                          shop.Id);

            Assert.True(product.IsAvailable);

            shop.ChangeProductAvailability(product.Id);
            
            Assert.False(product.IsAvailable);
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
