using Application.Features.Orders;
using Domain.Customers.Entities.Orders;
using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.ValueObjects;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.ShopOrders;
using Domain.Shops.Entities.ShopOrders.ValueObjects;
using Domain.Shops.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShopOrders
{
    public record ShopOrderDetailsDto
    {
        public Guid Id { get; init; }
        public Guid ShopId { get; init; }
        public Guid CustomerId { get; init; }
        public Guid OrderId { get; init; }
        public Address ShippingAddress { get; init; }
        public MoneyValue TotalPrice { get; init; }
        public string ShopOrderStatus { get; init; }
        public DateTime PlacedOn { get; init; }
        public DateTime? StatusChanged { get; init; } = null;
        public List<ShopOrderItemDto> ShopOrderItems { get; init; }

        public static ShopOrderDetailsDto CreateShopOrderDtoFromObject(ShopOrder shopOrder)
        {
            var orderItemList = new List<ShopOrderItemDto>();

            foreach (var orderItem in shopOrder.ShopOrderItems)
            {
                var orderItemDto = ShopOrderItemDto.CreateShopOrderItemDtoFromObject(orderItem);

                orderItemList.Add(orderItemDto);
            }

            return new ShopOrderDetailsDto()
            {
                Id = shopOrder.Id,
                ShopId = shopOrder.ShopId,
                CustomerId = shopOrder.CustomerId,
                OrderId = shopOrder.OrderId,
                ShippingAddress = shopOrder.ShippingAddress,
                TotalPrice = shopOrder.TotalPrice,
                ShopOrderStatus = shopOrder.OrderStatus.ToString(),
                PlacedOn = shopOrder.PlacedOn,
                StatusChanged = shopOrder.StatusChanged,
                ShopOrderItems = orderItemList
            };

        }
    }
}
