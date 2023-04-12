using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Customers.ValueObjects;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entities.Orders
{
    public class Order
    {
        public OrderId Id { get; private set; }
        public OrderStatus OrderStatus { get; private set; } = OrderStatus.InProgress;
        public CustomerId CustomerId { get; private set; }
        public MoneyValue TotalPrice { get; private set; }
        public Address ShippingAddress { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }
        //public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.WaitingForPayment;
        public DateTime PlacedOn { get; private set; } = DateTime.UtcNow;

        private Order() 
        {
            OrderItems = new List<OrderItem>();
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

            var orderItemsList = new List<OrderItem>();
            var cartItemsList = shoppingCart.Items;

            foreach (var cartItem in cartItemsList) 
            {
                var orderItem = OrderItem.CreateFromShoppingCartItem(this.Id, cartItem);
                orderItemsList.Add(orderItem);
            }

            OrderItems = orderItemsList;
        }

        internal static Order CreateNew(ShoppingCart shoppingCart,
                                        Address shippingAddress,
                                        DateTime placedOn) 
        {
            return new Order(shoppingCart, shippingAddress, placedOn);
        }
        internal void CancelOrder() 
        {
            if (this.OrderStatus == OrderStatus.WaitingForPayment || this.OrderStatus == OrderStatus.InProgress)
            //if (this.PaymentStatus == PaymentStatus.WaitingForPayment)
            {
                this.OrderStatus = OrderStatus.Cancelled;
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
    }
}
