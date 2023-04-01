using Domain.Customers.Entities.ShoppingCarts.ValueObjects;
using Domain.Customers.ValueObjects;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entities.ShoppingCarts
{
    public class ShoppingCart
    {
        public ShoppingCartId Id { get; private set; }
        public CustomerId CustomerId { get; private set; }
        public List<ShoppingCartItem> Items { get; private set; }
        public MoneyValue TotalPrice { get; private set; }

        private ShoppingCart(ShoppingCartId id, CustomerId customerId)
        {
            Id = id;
            CustomerId = customerId;
            Items = new List<ShoppingCartItem>();
            TotalPrice = CountTotalPrice(Items);
            Items = new List<ShoppingCartItem>();
        }

        public static ShoppingCart CreateShoppingCart(ShoppingCartId Id, CustomerId customerId)
        {
            return new ShoppingCart(Id, customerId);
        }

        private MoneyValue CountTotalPrice(List<ShoppingCartItem> items) 
        {
            decimal allProductsPrice = items.Sum(x => x.Value.Amount);

            return MoneyValue.Of(allProductsPrice, TotalPrice.Currency);
        }

        internal MoneyValue GetPrice()
        {
            return TotalPrice;
        }

        public void AddProductToShoppingCart(ProductId productId, int quantity, decimal price)
        {
            var shoppingCartItem = ShoppingCartItem.CreateShoppingCartItemFromProduct(Guid.NewGuid(),
                                                                                      productId,
                                                                                      Id,
                                                                                      quantity,
                                                                                      price);

            Items.Add(shoppingCartItem);
        }
    }
}
