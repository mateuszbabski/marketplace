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
        public MoneyValue Price { get; private set; }
        public ProductUnit Unit { get; private set; }
        public ShopId ShopId { get; private set; }
        public bool IsAvailable { get; private set; } = true;
        public Shop Shop { get; private set; }

        private Product() { }
        internal Product(ProductId id,
                         ProductName productName,
                         ProductDescription productDescription,
                         MoneyValue price,
                         ProductUnit unit,
                         ShopId shopId)
        {
            Id = id;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            Unit = unit;
            ShopId = shopId;
            IsAvailable = true;
        }

        public void SetPrice(MoneyValue price)
        {
            Price = price;
        }

        public void SetUnit(ProductUnit unit)
        {
            Unit = unit;
        }

        public void SetName(ProductName productName)
        {
            ProductName = productName;
        }

        public void SetDescription(ProductDescription productDescription)
        {
            ProductDescription = productDescription;
        }

        internal MoneyValue GetPrice()
        {
            return Price;
        }

        public void Remove()
        {
            IsAvailable = false;
        }

        public void Restore()
        {
            IsAvailable = true;
        }

    }
}
