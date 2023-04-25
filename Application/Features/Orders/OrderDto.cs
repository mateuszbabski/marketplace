using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.Entities.Orders;
using Domain.Customers.ValueObjects;
using Domain.Shared.ValueObjects;

namespace Application.Features.Orders
{
    public record OrderDto
    {
        public Guid Id { get; init; }
        public string OrderStatus { get; init; }
        public Guid CustomerId { get; init; }
        public MoneyValue TotalPrice { get; init; }
        public DateTime PlacedOn { get; init; }

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