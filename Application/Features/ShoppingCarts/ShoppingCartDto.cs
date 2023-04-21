using Domain.Customers.Entities.ShoppingCarts.ValueObjects;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Customers.ValueObjects;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShoppingCarts
{
    public record ShoppingCartDto
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public List<ShoppingCartItemDto> Items { get; private set; }
        public MoneyValue TotalPrice { get; private set; }

        public static ShoppingCartDto CreateShoppingCartDtoFromObject(ShoppingCart shoppingCart)
        {
            var shoppingCartItemsList = new List<ShoppingCartItemDto>();

            foreach (var item in shoppingCart.Items)
            {
                var shoppingCartItemDto = ShoppingCartItemDto.CreateCartItemDtoFromObject(item);

                shoppingCartItemsList.Add(shoppingCartItemDto);
            }

            return new ShoppingCartDto()
            {
                Id = shoppingCart.Id,
                CustomerId = shoppingCart.CustomerId,
                TotalPrice = shoppingCart.TotalPrice,
                Items = shoppingCartItemsList
            };
        }
    }
}
