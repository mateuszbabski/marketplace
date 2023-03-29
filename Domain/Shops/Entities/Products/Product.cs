using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products.ValueObjects;
using Domain.Shops.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shops.Entities.Products
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
        public virtual Shop Shop { get; private set; }

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

        internal static Product CreateProduct(ProductId id,
                              ProductName productName,
                              ProductDescription productDescription,
                              MoneyValue price,
                              ProductUnit unit,
                              ShopId shopId)
        {
            return new Product(id, productName, productDescription, price, unit, shopId);
        }
        
        internal void ChangeDetails(string productName, string productDescription, string unit)
        {
            SetName(productName);
            SetDescription(productDescription);
            SetUnit(unit);
        }

        internal void ChangeAvailability()
        {
            if (IsAvailable == true)
            {
                Remove();
            }
            else
            {
                Restore();
            }
        }

        internal void SetPrice(MoneyValue price)
        {
            Price = price;
        }

        internal void SetUnit(string unit)
        {
            if (!string.IsNullOrEmpty(unit))
                Unit = new ProductUnit(unit);
        }

        internal void SetName(string productName)
        {
            if (!string.IsNullOrEmpty(productName))
                ProductName = new ProductName(productName);
        }

        internal void SetDescription(string productDescription)
        {
            if (!string.IsNullOrEmpty(productDescription))
                ProductDescription = new ProductDescription(productDescription);
        }

        public MoneyValue GetPrice()
        {
            return Price;
        }

        internal void Remove()
        {
            IsAvailable = false;
        }

        internal void Restore()
        {
            IsAvailable = true;
        }

    }
}
