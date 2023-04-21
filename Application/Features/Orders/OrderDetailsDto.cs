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
        public Guid Id { get; private set; }
        public string OrderStatus { get; private set; }
        public Guid CustomerId { get; private set; }
        public MoneyValue TotalPrice { get; private set; }
        public Address ShippingAddress { get; private set; }
        public List<OrderItemDto> OrderItems { get; private set; }
        public DateTime PlacedOn { get; private set; }

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
