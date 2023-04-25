using Domain.Customers.Entities.Orders;
using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders
{
    public record OrderItemDto
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public MoneyValue Price { get; init; }

        public static OrderItemDto CreateOrderItemDtoFromObject(OrderItem orderItem)
        {
            return new OrderItemDto()
            {
                Id = orderItem.Id,
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price,
            };
        }
    }
}
