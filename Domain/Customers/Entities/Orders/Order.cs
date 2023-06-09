﻿using Domain.Customers.Entities.Orders.Events;
using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Customers.ValueObjects;
using Domain.Shared.Abstractions;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.ShopOrders;
using System.Text.Json.Serialization;

namespace Domain.Customers.Entities.Orders
{
    public class Order : Entity
    {
        public OrderId Id { get; private set; }
        public OrderStatus OrderStatus { get; private set; } = OrderStatus.InProgress;
        public CustomerId CustomerId { get; private set; }
        public MoneyValue TotalPrice { get; private set; }
        public Address ShippingAddress { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }
        public DateTime PlacedOn { get; private set; } = DateTime.UtcNow;
        public DateTime? StatusChanged { get; private set; } = null;
        public List<ShopOrder> ShopOrders { get; private set; }
        [JsonIgnore]
        public virtual Customer Customer { get; private set; }

        private Order() 
        {
            OrderItems = new List<OrderItem>();
            ShopOrders = new List<ShopOrder>();
        }

        private Order(ShoppingCart shoppingCart,
                      Address shippingAddress,
                      DateTime placedOn)
        {
            Id = new OrderId(Guid.NewGuid());              
            OrderStatus = OrderStatus.WaitingForPayment;
            PlacedOn = placedOn;
            CustomerId = shoppingCart.CustomerId;
            TotalPrice = shoppingCart.TotalPrice;
            ShippingAddress = shippingAddress;
            OrderItems = new List<OrderItem>();
            ShopOrders = new List<ShopOrder>();

            var cartItemsList = shoppingCart.Items;

            foreach (var cartItem in cartItemsList) 
            {
                var orderItem = OrderItem.CreateFromShoppingCartItem(this.Id, cartItem);
                OrderItems.Add(orderItem);
            }            
        }

        internal static Order CreateNew(ShoppingCart shoppingCart,
                                        Address shippingAddress,
                                        DateTime placedOn)
        {
            var order = new Order(shoppingCart, shippingAddress, placedOn);

            order.AddDomainEvent(new OrderCreatedDomainEvent(order));

            SplitOrderByShops(order, shoppingCart);            

            return order;
        }

        internal void CancelOrder()
        {
            if (this.OrderStatus == OrderStatus.WaitingForPayment || this.OrderStatus == OrderStatus.InProgress)
            {
                this.StatusChanged = DateTime.Now;
                this.OrderStatus = OrderStatus.Cancelled;

                this.AddDomainEvent(new OrderCancelledDomainEvent(this.Id));
            }
        }

        internal MoneyValue GetPrice() 
        {
            return TotalPrice;
        }

        internal void ChangeShippingAddress(Address shippingAddress)
        {
            if(shippingAddress is not null)
                ShippingAddress = shippingAddress;
        }

        private static void SplitOrderByShops(Order order, ShoppingCart shoppingCart)
        {
            foreach (var productByShopList in shoppingCart.Items.GroupBy(x => x.ShopId))
            {
                var productList = new List<ShoppingCartItem>();

                var productsToAdd = productByShopList.ToList();

                productList.AddRange(productsToAdd);

                var shopOrder = ShopOrder.CreateShopOrder(order, productList);

                order.ShopOrders.Add(shopOrder);
            }

            order.AddDomainEvent(new OrderSplitDomainEvent(order));
        }
    }
}
