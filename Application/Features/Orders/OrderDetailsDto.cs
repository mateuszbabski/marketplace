using Domain.Customers.Entities.Orders;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }
        public string OrderStatus { get; set; }
        public Guid CustomerId { get; set; }
        public MoneyValue TotalPrice { get; set; }
        public Address ShippingAddress { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public DateTime PlacedOn { get; set; }
    }
}
