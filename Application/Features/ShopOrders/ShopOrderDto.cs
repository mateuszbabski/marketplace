using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.ShopOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShopOrders
{
    public record ShopOrderDto
    {
        public Guid Id { get; init; }
        public Guid CustomerId { get; init; }
        public Guid ShopId { get; init; }
        public MoneyValue TotalPrice { get; init; }
        public string ShopOrderStatus { get; init; }
        public DateTime PlacedOn { get; init; }

        //public static ShopOrderDto CreateShopOrderDtoFromObject(ShopOrder shopOrder)
        //{
        //    return new ShopOrderDto()
        //    {
        //        Id = shopOrder.Id,
        //        CustomerId = shopOrder.CustomerId,
        //        TotalPrice = shopOrder.TotalPrice,
        //        ShopOrderStatus = shopOrder.ShopOrderStatus.ToString(),
        //        PlacedOn = shopOrder.PlacedOn,
        //    };

        //}

        public static IEnumerable<ShopOrderDto> CreateShopOrderDtoFromObject(IEnumerable<ShopOrder> shopOrders)
        {
            var orderList = new List<ShopOrderDto>();

            foreach (var shopOrder in shopOrders)
            {
                var shopOrderDto = new ShopOrderDto()
                {
                    Id = shopOrder.Id,
                    ShopId = shopOrder.ShopId,
                    CustomerId = shopOrder.CustomerId,
                    TotalPrice = shopOrder.TotalPrice,
                    ShopOrderStatus = shopOrder.OrderStatus.ToString(),
                    PlacedOn = shopOrder.PlacedOn,
                };

                orderList.Add(shopOrderDto);
            }
            return orderList;
        }
    }
}
