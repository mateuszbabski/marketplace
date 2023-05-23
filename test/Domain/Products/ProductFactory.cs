using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products;
using UnitTest.Domain.Shops;

namespace UnitTest.Domain.Products
{
    public class ProductFactory
    {
        public static Product CreateProduct()
        {
            var shop = ShopFactory.Create();

            var product = shop.AddProduct("productName",
                                          "productDescription",
                                          MoneyValue.Of(10, "USD"),
                                          "pieces",
                                          shop.Id);

            return product;
        }
    }
}
