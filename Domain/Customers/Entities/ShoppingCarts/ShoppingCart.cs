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
            TotalPrice = new MoneyValue(0, "PLN");
        }

        public static ShoppingCart CreateShoppingCart(ShoppingCartId Id, CustomerId customerId)
        {
            return new ShoppingCart(Id, customerId);
        }

        private MoneyValue CountTotalPrice(List<ShoppingCartItem> items) 
        {
            decimal allProductsPrice = items.Sum(x => x.Price.Amount);

            return new MoneyValue(allProductsPrice, TotalPrice.Currency);
            //return MoneyValue.Of(allProductsPrice, TotalPrice.Currency);
        }

        internal MoneyValue GetPrice()
        {
            return TotalPrice;
        }

        public void AddProductToShoppingCart(ProductId productId, int quantity, MoneyValue productPrice)
        {
            //TODO CONVERT PRODUCT MONEYVALUE TO SHOPPINGCART CURRENCY
            //TODO WHILE CREATING ORDER CHOOSE CURRENCY AND CONVERT ALL PRICES
            var shoppingCartItem = Items.FirstOrDefault(x => x.ProductId == productId);

            if (shoppingCartItem == null)
            {
                var newShoppingCartItem = ShoppingCartItem.CreateShoppingCartItemFromProduct(Guid.NewGuid(),
                                                                                          productId,
                                                                                          Id,
                                                                                          quantity,
                                                                                          productPrice);
                Items.Add(newShoppingCartItem);
            }
            else
            {
                shoppingCartItem.ChangeCartItemQuantity(quantity, productPrice);
            }

            this.TotalPrice = CountTotalPrice(this.Items);
        }

        public void RemoveItemFromCart(ShoppingCartItemId shoppingCartItemId)
        {
            var item = Items.FirstOrDefault(x => x.Id == shoppingCartItemId);

            Items.Remove(item);        

            this.TotalPrice = CountTotalPrice(this.Items);
        }
    }
}
