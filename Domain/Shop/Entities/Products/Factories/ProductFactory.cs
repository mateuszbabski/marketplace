using Domain.Shared.ValueObjects;
using Domain.Shop.Entities.Products.ValueObjects;
using Domain.Shop.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shop.Entities.Products.Factories
{
    public sealed class ProductFactory : IProductFactory
    {
        public Product Create(ProductId id,
                              ProductName productName,
                              ProductDescription productDescription,
                              ProductPrice productPrice,
                              ProductUnit unit,
                              ShopId shopId) =>
            new(id, productName, productDescription, productPrice, unit, shopId);
    }
}
