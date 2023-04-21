using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.Entities.Orders;
using Domain.Customers.ValueObjects;
using Domain.Shared.ValueObjects;

namespace Application.Features.Orders
{
    public record OrderDto
    {
        public Guid Id { get; private set; }
        public string OrderStatus { get; private set; }
        public Guid CustomerId { get; private set; }
        public MoneyValue TotalPrice { get; private set; }
        public DateTime PlacedOn { get; private set; }

        public static OrderDto CreateOrderDtoFromObject(Order order)
        {
            return new OrderDto()
            {
                Id = order.Id,
                OrderStatus = order.OrderStatus.ToString(),
                CustomerId = order.CustomerId,
                TotalPrice = order.TotalPrice,
                PlacedOn = order.PlacedOn
            };
        }
    }
}