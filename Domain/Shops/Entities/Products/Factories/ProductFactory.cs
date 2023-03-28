using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products.ValueObjects;
using Domain.Shops.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shops.Entities.Products.Factories
{
    public sealed class ProductFactory : IProductFactory
    {
        public Product Create(ProductId id,
                              ProductName productName,
                              ProductDescription productDescription,
                              MoneyValue price,
                              ProductUnit unit,
                              ShopId shopId) =>
            new(id, productName, productDescription, price, unit, shopId);
    }
}
