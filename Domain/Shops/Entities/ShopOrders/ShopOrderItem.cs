using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.Entities.Orders;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Shops.Entities.ShopOrders.ValueObjects;
using Domain.Customers.Entities.ShoppingCarts;

namespace Domain.Shops.Entities.ShopOrders
{
    public class ShopOrderItem
    {
        public ShopOrderItemId Id { get; private set; }
        public ShopOrderId ShopOrderId { get; private set; }
        public ProductId ProductId { get; private set; }
        public int Quantity { get; private set; }
        public MoneyValue Price { get; private set; }
        [JsonIgnore]
        public virtual ShopOrder ShopOrder { get; set; }

        private ShopOrderItem() { }
        private ShopOrderItem(ShopOrderId shopOrderId, ShoppingCartItem shoppingCartItem) 
        {
            Id = Guid.NewGuid();
            ShopOrderId = shopOrderId;
            ProductId = shoppingCartItem.ProductId;
            Quantity = shoppingCartItem.Quantity;
            Price = shoppingCartItem.Price;
        }

        public static ShopOrderItem CreateShopOrderItem(ShopOrderId shopOrderId, ShoppingCartItem shoppingCartItem)
        {
            return new ShopOrderItem(shopOrderId, shoppingCartItem);
        }
    }
}
