using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Shared.Abstractions;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products.ValueObjects;
using Domain.Shops.ValueObjects;
using System.Text.Json.Serialization;

namespace Domain.Customers.Entities.Orders
{
    public class OrderItem : Entity
    {
        public OrderItemId Id { get; private set; }
        public OrderId OrderId {  get; private set; }
        public ProductId ProductId { get; private set; }
        public int Quantity { get; private set; }
        public MoneyValue Price { get; private set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }

        private OrderItem() { }

        private OrderItem(OrderId orderId, ShoppingCartItem shoppingCartItem)
        {
            Id = new OrderItemId(Guid.NewGuid());
            OrderId = orderId;
            ProductId = shoppingCartItem.ProductId;
            Quantity = shoppingCartItem.Quantity;
            Price = shoppingCartItem.Price;
        }

        internal static OrderItem CreateFromShoppingCartItem(OrderId orderId, ShoppingCartItem shoppingCartItem)
        {            
            return new OrderItem(orderId, shoppingCartItem);
        }
    }
}
