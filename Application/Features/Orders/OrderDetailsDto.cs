using Domain.Customers.Entities.Orders;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders
{
    public record OrderDetailsDto
    {
        public Guid Id { get; init; }
        public string OrderStatus { get; init; }
        public Guid CustomerId { get; init; }
        public MoneyValue TotalPrice { get; init; }
        public Address ShippingAddress { get; init; }
        public List<OrderItemDto> OrderItems { get; init; }
        public DateTime PlacedOn { get; init; }

        public static OrderDetailsDto CreateOrderDetailsDtoFromObject(Order order)
        {
            var orderItemList = new List<OrderItemDto>();

            foreach (var orderItem in order.OrderItems)
            {
                var orderItemDto = OrderItemDto.CreateOrderItemDtoFromObject(orderItem);

                orderItemList.Add(orderItemDto);
            }

            return new OrderDetailsDto()
            {
                Id = order.Id,
                OrderStatus = order.OrderStatus.ToString(),
                CustomerId = order.CustomerId,
                TotalPrice = order.TotalPrice,
                ShippingAddress = order.ShippingAddress,
                PlacedOn = order.PlacedOn,
                OrderItems = orderItemList
            };
        }
    }
}
