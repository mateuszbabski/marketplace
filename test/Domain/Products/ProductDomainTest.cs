using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.Products.Exceptions;
using UnitTest.Domain.Shops;

namespace UnitTest.Domain.Products
{
    public class ProductDomainTest
    {        
        private static Product CreateProduct()
        {
            var shop = ShopFactory.Create();

            var product = shop.AddProduct("productName",
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

            var product = shop.AddProduct("productName",
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

            var act = Assert.Throws<EmptyProductNameException>(() => shop.AddProduct("",
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

            var act = Assert.Throws<InvalidProductPriceException>(() => shop.AddProduct("productName",
                                          "productDescription",
                                          MoneyValue.Of(10, "USD"),
                                          "pieces",
                                          shop.Id));

            Assert.IsType<InvalidProductPriceException>(act);
            Assert.Equal("Invalid currency.", act.Message);
        }

        [Fact]
        public void GetProductPrice_ReturnsProductPrice()
        {
            var shop = ShopFactory.Create();

            var product = shop.AddProduct("productName",
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
