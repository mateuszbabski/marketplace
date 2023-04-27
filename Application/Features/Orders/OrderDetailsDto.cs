using Application.Features.ShopOrders;
using Domain.Customers.Entities.Orders;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.ShopOrders;
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
        public DateTime PlacedOn { get; init; }
        public DateTime? StatusChanged { get; init; }
        public List<OrderItemDto> OrderItems { get; init; }
        public List<ShopOrderDto> ShopOrders { get; init; }

        public static OrderDetailsDto CreateOrderDetailsDtoFromObject(Order order)
        {
            var orderItemList = new List<OrderItemDto>();

            foreach (var orderItem in order.OrderItems)
            {
                var orderItemDto = OrderItemDto.CreateOrderItemDtoFromObject(orderItem);

                orderItemList.Add(orderItemDto);
            }

            var shopOrderList = ShopOrderDto.CreateShopOrderDtoFromObject(order.ShopOrders);

            return new OrderDetailsDto()
            {
                Id = order.Id,
                OrderStatus = order.OrderStatus.ToString(),
                CustomerId = order.CustomerId,
                TotalPrice = order.TotalPrice,
                ShippingAddress = order.ShippingAddress,
                PlacedOn = order.PlacedOn,
                StatusChanged = order.StatusChanged,
                OrderItems = orderItemList,
                ShopOrders = shopOrderList.ToList(),
            };
        }
    }
}
