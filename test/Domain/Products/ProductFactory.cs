using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.Products.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Domain.Products
{
    public class ProductTestFactory
    {
        private readonly IProductFactory _productFactory;

        public ProductTestFactory(IProductFactory productFactory)
        {
            _productFactory = productFactory;
        }
        public Product CreateProduct()
        {
            var productId = Guid.NewGuid();
            var productName = "Product";
            var productDescription = "Description";
            var productUnit = "pcs";
            var prouductPrice = MoneyValue.Of(10, "PLN");
            var shopId = Guid.NewGuid();

            var testProduct = _productFactory.Create(productId,
                                         productName,
                                         productDescription,
                                         prouductPrice,
                                         productUnit,
                                         shopId);

            return testProduct;
        }        
    }
}
