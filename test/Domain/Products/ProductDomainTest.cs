using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.Products.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Domain.Shops;

namespace UnitTest.Domain.Products
{
    public class ProductDomainTest
    {        
        private static Product CreateProduct()
        {
            var shop = ShopFactory.Create();

            var product = shop.AddProduct(Guid.NewGuid(),
                                          "productName",
                                          "productDescription",
                                          MoneyValue.Of(10, "USD"),
                                          "pieces",
                                          shop.Id);

            return product;
        }

        [Fact]
        public void CanCreateProduct_ReturnsProductIfCreatedSuccessfully()
        {
            var shop = ShopFactory.Create();

            var product = shop.AddProduct(Guid.NewGuid(),
                                          "productName",
                                          "productDescription",
                                          MoneyValue.Of(10, "USD"),
                                          "pieces",
                                          shop.Id);

            Assert.NotNull(product);
            Assert.IsType<Product>(product);
            Assert.Equal("productName", product.ProductName);
            Assert.Equal(product.ShopId, shop.Id);
        }

        [Fact]
        public void CreateProduct_ThrowsInvalidNameWhenNameParamIsEmpty()
        {
            var shop = ShopFactory.Create();

            var act = Assert.Throws<EmptyProductNameException>(() => shop.AddProduct(Guid.NewGuid(),
                                          "",
                                          "productDescription",
                                          MoneyValue.Of(10, "USD"),
                                          "pieces",
                                          shop.Id));            
            
            Assert.IsType<EmptyProductNameException>(act);
            Assert.Equal("Product name cannot be empty.", act.Message);
        }

        [Fact]
        public void CreateProduct_ThrowsInvalidProductPriceWhenCurrencyNameIsInvalid()
        {
            var shop = ShopFactory.Create();

            var act = Assert.Throws<InvalidProductPriceException>(() => shop.AddProduct(Guid.NewGuid(),
                                          "productName",
                                          "productDescription",
                                          MoneyValue.Of(10, "USDC"),
                                          "pieces",
                                          shop.Id));

            Assert.IsType<InvalidProductPriceException>(act);
            Assert.Equal("Invalid currency.", act.Message);
        }

        [Fact]
        public void ChangeProductDetails_DoNotChangeWhenParamsAreEmpty()
        {
            var shop = ShopFactory.Create();

            var product = shop.AddProduct(Guid.NewGuid(),
                                          "productName",
                                          "productDescription",
                                          MoneyValue.Of(10, "USD"),
                                          "pieces",
                                          shop.Id);

            shop.ChangeProductDetails(product.Id, "", "", "");

            Assert.Equal("productName", product.ProductName);
            Assert.Equal("productDescription", product.ProductDescription);
            Assert.Equal("pieces", product.Unit);
        }

        [Fact]
        public void ChangeProductPrice_ProductPriceChangedIfParamsAreValid()
        {
            var shop = ShopFactory.Create();

            var product = shop.AddProduct(Guid.NewGuid(),
                                          "productName",
                                          "productDescription",
                                          MoneyValue.Of(10, "USD"),
                                          "pieces",
                                          shop.Id);

            shop.ChangeProductPrice(product.Id, 20, "CAD");

            Assert.Equal(20, product.Price.Amount);
            Assert.Equal("CAD", product.Price.Currency);
        }

        [Fact]
        public void ChangeProductAvailability_ProductAvailability()
        {
            var shop = ShopFactory.Create();

            var product = shop.AddProduct(Guid.NewGuid(),
                                          "productName",
                                          "productDescription",
                                          MoneyValue.Of(10, "USD"),
                                          "pieces",
                                          shop.Id);

            Assert.True(product.IsAvailable);

            shop.ChangeProductAvailability(product.Id);

            Assert.False(product.IsAvailable);
        }

        [Fact]
        public void GetProductPrice_ReturnsProductPrice()
        {
            var shop = ShopFactory.Create();

            var product = shop.AddProduct(Guid.NewGuid(),
                                          "productName",
                                          "productDescription",
                                          MoneyValue.Of(10, "USD"),
                                          "pieces",
                                          shop.Id);

            var price = product.GetPrice();

            Assert.IsType<MoneyValue>(price);
            Assert.Equal(10, price.Amount);
            Assert.Equal("USD", price.Currency);
        }
    }
}
