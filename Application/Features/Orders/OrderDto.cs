﻿using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.Entities.Orders;
using Domain.Customers.ValueObjects;
using Domain.Shared.ValueObjects;

namespace Application.Features.Orders
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string OrderStatus { get; set; }
        public Guid CustomerId { get; set; }
        public MoneyValue TotalPrice { get; set; }
        public DateTime PlacedOn { get; set; }
    }
}