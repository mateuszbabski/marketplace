using Domain.Shared.ValueObjects;
using Domain.Shop.Entities.Products.ValueObjects;
using Domain.Shop.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shop.Entities.Products
{
    public class Product
    {
        public ProductId Id { get; private set; }
        public ProductName ProductName { get; private set; }
        public ProductDescription ProductDescription { get; private set; }
        public ProductPrice ProductPrice { get; private set; }
        public Unit Unit { get; private set; }

        public ShopId ShopId { get; private set; }
        public bool IsAvailable { get; private set; } = true;

        private Product() { }
        internal Product(ProductId id,
                         ProductName productName,
                         ProductDescription productDescription,
                         ProductPrice productPrice,
                         Unit unit,
                         ShopId shopId)
        {
            Id = id;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
            Unit = unit;
            ShopId = shopId;
            IsAvailable = true;
        }
    }
}
