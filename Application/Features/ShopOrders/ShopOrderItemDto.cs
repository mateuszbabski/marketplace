using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products.ValueObjects;
using Domain.Shops.Entities.ShopOrders;
using Domain.Shops.Entities.ShopOrders.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShopOrders
{
    public record ShopOrderItemDto
    {
        public Guid Id { get; init; }
        public Guid ShopOrderId { get; init; }
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public MoneyValue Price { get; init; }
        public static ShopOrderItemDto CreateShopOrderItemDtoFromObject(ShopOrderItem shopOrderItem)
        {
            return new ShopOrderItemDto()
            {
                Id = shopOrderItem.Id,
                ShopOrderId = shopOrderItem.ShopOrderId,
                ProductId = shopOrderItem.ProductId,
                Quantity = shopOrderItem.Quantity,
                Price = shopOrderItem.Price,
            };
        }
    }
}
