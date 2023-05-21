﻿using Domain.Customers.Entities.ShoppingCarts.Exceptions;
using Domain.Customers.Entities.ShoppingCarts.ValueObjects;
using Domain.Shared.Abstractions;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.Products.ValueObjects;
using Domain.Shops.ValueObjects;
using System.Text.Json.Serialization;

namespace Domain.Customers.Entities.ShoppingCarts
{
    public class ShoppingCartItem : Entity
    {
        public ShoppingCartItemId Id { get; private set; }
        public ProductId ProductId { get; private set; }
        public ShopId ShopId { get; private set; }
        public ShoppingCartId ShoppingCartId { get; private set; }
        public int Quantity { get; private set; } = 1;
        public MoneyValue Price { get; private set; }

        [JsonIgnore]
        public virtual ShoppingCart ShoppingCart { get; set; }

        private ShoppingCartItem() { }

        private ShoppingCartItem(Product product,
                                 ShoppingCartId shoppingCartId,
                                 int quantity,
                                 string currency,
                                 decimal convertedPrice)
        {
            Id = new ShoppingCartItemId(Guid.NewGuid());
            ProductId = product.Id;
            ShoppingCartId = shoppingCartId;
            ShopId = product.ShopId;
            Quantity = quantity;
            Price = CountCartItemPrice(quantity, convertedPrice, currency);
        }

        internal static ShoppingCartItem CreateShoppingCartItemFromProduct(Product product,
                                                                           ShoppingCartId shoppingCartId,
                                                                           int quantity,
                                                                           string currency,
                                                                           decimal convertedPrice)
        {
            if (quantity <= 0)
            {
                throw new InvalidQuantityException();
            }

            return new ShoppingCartItem(product, shoppingCartId, quantity, currency, convertedPrice);
        }

        private static MoneyValue CountCartItemPrice(int quantity, decimal convertedPrice, string currency)
        {
            return MoneyValue.Of(quantity * convertedPrice, currency);
        }

        internal void ChangeCartItemQuantity(int quantity, decimal convertedPrice, string currency)
        {
            if (quantity <= 0)
            {
                throw new InvalidQuantityException();
            }

            this.Quantity = quantity;

            this.Price = CountCartItemPrice(quantity, convertedPrice, currency);
        }
    }
}
