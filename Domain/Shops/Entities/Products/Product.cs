﻿using Domain.Shared.Abstractions;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products.Events;
using Domain.Shops.Entities.Products.ValueObjects;
using Domain.Shops.ValueObjects;
using System.Text.Json.Serialization;

namespace Domain.Shops.Entities.Products
{
    public class Product : Entity
    {
        public ProductId Id { get; private set; }
        public ProductName ProductName { get; private set; }
        public ProductDescription ProductDescription { get; private set; }
        public MoneyValue Price { get; private set; }
        public ProductUnit Unit { get; private set; }
        public ShopId ShopId { get; private set; }
        public bool IsAvailable { get; private set; } = true;
        [JsonIgnore]
        public virtual Shop Shop { get; private set; }

        private Product() { }
        internal Product(ProductName productName,
                         ProductDescription productDescription,
                         MoneyValue price,
                         ProductUnit unit,
                         ShopId shopId)
        {
            Id = new ProductId(Guid.NewGuid());
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            Unit = unit;
            ShopId = shopId;
            IsAvailable = true;
        }

        internal static Product CreateProduct(ProductName productName,
                                              ProductDescription productDescription,
                                              MoneyValue price,
                                              ProductUnit unit,
                                              ShopId shopId)
        {
            var product = new Product(productName, productDescription, price, unit, shopId);

            product.AddDomainEvent(new ProductAddedToShopDomainEvent(product));

            return product;
        }
        
        internal void ChangeDetails(string productName, string productDescription, string unit)
        {
            SetName(productName);
            SetDescription(productDescription);
            SetUnit(unit);

            this.AddDomainEvent(new ProductDetailsChangedDomainEvent(this));
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
            this.AddDomainEvent(new ProductPriceChangedDomainEvent(this));
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
            this.AddDomainEvent(new ProductRemovedFromShopDomainEvent(this));
        }

        internal void Restore()
        {
            IsAvailable = true;
            this.AddDomainEvent(new ProductRestoredDomainEvent(this));
        }

    }
}
