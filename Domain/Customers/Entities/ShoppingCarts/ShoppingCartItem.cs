using Domain.Customers.Entities.ShoppingCarts.Exceptions;
using Domain.Customers.Entities.ShoppingCarts.ValueObjects;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.Products.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entities.ShoppingCarts
{
    public class ShoppingCartItem
    {
        public ShoppingCartItemId Id { get; set; }
        public ProductId ProductId { get; private set; }
        public ShoppingCartId ShoppingCartId { get; private set; }
        public int Quantity { get; private set; } = 1;
        public MoneyValue Value { get; private set; }
        //public virtual Product Product { get; private set; }
        public virtual ShoppingCart ShoppingCart { get; set; }

        private ShoppingCartItem() { }
        private ShoppingCartItem(ShoppingCartItemId id, ProductId productId, ShoppingCartId shoppingCartId, int quantity, decimal productPrice)
        {
            Id = id;
            ProductId = productId;
            ShoppingCartId = shoppingCartId;
            Quantity = quantity;
            Value = CountCartItemPrice(quantity, productPrice);
        }

        internal static ShoppingCartItem CreateShoppingCartItemFromProduct(ShoppingCartItemId id,
                                                                           ProductId productId,
                                                                           ShoppingCartId shoppingCartId,
                                                                           int quantity,
                                                                           decimal productPrice)
        {
            return new ShoppingCartItem(id, productId, shoppingCartId, quantity, productPrice);
        }        

        private MoneyValue CountCartItemPrice(int quantity, decimal productPrice) 
        {
            return MoneyValue.Of(quantity * productPrice, Value.Currency);
        }

        internal void ChangeCartItemQuantity(int quantity, decimal productPrice)
        {
            if (quantity <= 0)
            {
                throw new InvalidQuantityException();
            }

            this.Quantity = quantity;

            this.Value = MoneyValue.Of(quantity * productPrice, Value.Currency);            
        }

    }
}
