using Domain.Customers.Entities.ShoppingCarts;
using Domain.Customers.Entities.ShoppingCarts.ValueObjects;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShoppingCarts
{
    public record ShoppingCartItemDto
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public MoneyValue Price { get; private set; }

        public static ShoppingCartItemDto CreateCartItemDtoFromObject(ShoppingCartItem shoppingCartItem)
        {
            return new ShoppingCartItemDto()
            {
                Id = shoppingCartItem.Id,
                ProductId = shoppingCartItem.ProductId,
                Quantity = shoppingCartItem.Quantity,
                Price = shoppingCartItem.Price,
            };
        }
    }
}
