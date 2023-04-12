using Domain.Customers.Entities.ShoppingCarts.Exceptions;
using Domain.Customers.Entities.ShoppingCarts.ValueObjects;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.Products.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Customers.Entities.ShoppingCarts
{
    public class ShoppingCartItem
    {
        public ShoppingCartItemId Id { get; set; }
        public ProductId ProductId { get; private set; }
        public ShoppingCartId ShoppingCartId { get; private set; }
        public int Quantity { get; private set; } = 1;
        public MoneyValue Price { get; private set; }

        [JsonIgnore]
        public virtual ShoppingCart ShoppingCart { get; set; }

        private ShoppingCartItem() { }
        private ShoppingCartItem(ProductId productId, ShoppingCartId shoppingCartId, int quantity, MoneyValue productPrice)
        {
            Id = new ShoppingCartItemId(Guid.NewGuid());
            ProductId = productId;
            ShoppingCartId = shoppingCartId;
            Quantity = quantity;
            Price = CountCartItemPrice(quantity, productPrice);
        }

        internal static ShoppingCartItem CreateShoppingCartItemFromProduct(ProductId productId,
                                                                           ShoppingCartId shoppingCartId,
                                                                           int quantity,
                                                                           MoneyValue productPrice)
        {
            if (quantity <= 0)
            {
                throw new InvalidQuantityException();
            }

            return new ShoppingCartItem(productId, shoppingCartId, quantity, productPrice);
        }        

        private static MoneyValue CountCartItemPrice(int quantity, MoneyValue productPrice) 
        {
            return MoneyValue.Of(quantity * productPrice.Amount, productPrice.Currency);
        }

        internal void ChangeCartItemQuantity(int quantity, MoneyValue productPrice)
        {
            if (quantity <= 0)
            {
                throw new InvalidQuantityException();
            }

            this.Quantity = quantity;

            this.Price = CountCartItemPrice(quantity, productPrice);
        }
    }
}
