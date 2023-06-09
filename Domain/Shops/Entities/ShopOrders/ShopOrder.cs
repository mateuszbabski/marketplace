﻿using Domain.Customers.ValueObjects;
using Domain.Customers;
using Domain.Shared.ValueObjects;
using System.Text.Json.Serialization;
using Domain.Shops.Entities.ShopOrders.ValueObjects;
using Domain.Shops.ValueObjects;
using Domain.Customers.Entities.Orders;
using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Shared.Abstractions;
using Domain.Shops.Entities.ShopOrders.Events;

namespace Domain.Shops.Entities.ShopOrders
{
    public class ShopOrder : Entity
    {
        public ShopOrderId Id { get; private set; }
        public ShopId ShopId { get; private set; }
        public CustomerId CustomerId { get; private set; }
        public OrderId OrderId { get; private set; }
        public Address ShippingAddress { get; private set; }
        public MoneyValue TotalPrice { get; private set; }
        public OrderStatus OrderStatus { get; private set; } = OrderStatus.InProgress;
        public DateTime PlacedOn { get; private set; } = DateTime.UtcNow;
        public DateTime? StatusChanged { get; private set; } = null;
        public List<ShopOrderItem> ShopOrderItems { get; private set; }

        [JsonIgnore]
        public virtual Shop Shop { get; private set; }

        [JsonIgnore]
        public virtual Order Order { get; private set; }

        private ShopOrder() 
        {
            ShopOrderItems = new List<ShopOrderItem>();
        }

        private ShopOrder(Order order, Address shippingAddress, List<ShoppingCartItem> shoppingCartItems) 
        {            
            Id = new ShopOrderId(Guid.NewGuid());            
            ShopId = shoppingCartItems.First().ShopId;
            CustomerId = order.CustomerId;
            OrderId = order.Id;
            ShippingAddress = shippingAddress;
            PlacedOn = order.PlacedOn;
            OrderStatus = OrderStatus.WaitingForPayment;
            ShopOrderItems = new List<ShopOrderItem>();

            var shopItemsList = shoppingCartItems;

            foreach (var shopItem in shopItemsList)
            {
                var orderItem = ShopOrderItem.CreateShopOrderItem(this.Id, shopItem);
                ShopOrderItems.Add(orderItem);
            }

            TotalPrice = CountTotalPrice(shopItemsList, order.TotalPrice.Currency);
        }

        public static ShopOrder CreateShopOrder(Order order, List<ShoppingCartItem> shoppingCartItems)
        {
            var shippingAddress = Address.CreateAddress(order.ShippingAddress.Country,
                                                order.ShippingAddress.City,
                                                order.ShippingAddress.Street,
                                                order.ShippingAddress.PostalCode);

            var shopOrder = new ShopOrder(order, shippingAddress, shoppingCartItems);

            shopOrder.AddDomainEvent(new ShopOrderCreatedDomainEvent(shopOrder));

            return shopOrder;
        }

        private static MoneyValue CountTotalPrice(List<ShoppingCartItem> items, string currency)
        {
            decimal allProductsPrice = items.Sum(x => x.Price.Amount);

            return MoneyValue.Of(allProductsPrice, currency);
        }

        public void CancelOrder()
        {
            if (this.OrderStatus == OrderStatus.WaitingForPayment || this.OrderStatus == OrderStatus.InProgress)
            {
                this.OrderStatus = OrderStatus.Cancelled;
                this.StatusChanged = DateTime.Now;
                this.AddDomainEvent(new ShopOrderCancelledDomainEvent(this));
            }            
        }
    }
}
