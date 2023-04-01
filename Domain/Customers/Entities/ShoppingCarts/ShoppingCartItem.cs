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
        public ProductId ProductId { get; private set; }
        public ShoppingCartId ShoppingCartId { get; private set; }
        public int Quantity { get; private set; } = 1;
        public MoneyValue Value { get; private set; }
        public virtual Product Product { get; private set; }
        public virtual ShoppingCart ShoppingCart { get; set; }

        private ShoppingCartItem(ProductId productId, ShoppingCartId shoppingCartId, int quantity)
        {
            ProductId = productId;
            ShoppingCartId = shoppingCartId;
            Quantity = quantity;
            Value = CountCartItemPrice(quantity, Product.Price.Amount);
        }

        internal static ShoppingCartItem CreateShoppingCartItemFromProduct(ProductId productId,
                                                                           ShoppingCartId shoppingCartId,
                                                                           int quantity)
        {
            return new ShoppingCartItem(productId, shoppingCartId, quantity);
        }        

        private MoneyValue CountCartItemPrice(int quantity, decimal productPrice) 
        {
            return MoneyValue.Of(quantity * productPrice, Product.Price.Currency);
        }

        internal void ChangeCartItemQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new InvalidQuantityException();
            }

            this.Quantity = quantity;

            this.Value = MoneyValue.Of(quantity * Product.Price.Amount, Product.Price.Currency);            
        }

    }
}
