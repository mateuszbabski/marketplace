using Domain.Customers.Entities.ShoppingCarts;
using Domain.Shared.ValueObjects;

namespace Application.Features.ShoppingCarts
{
    public record ShoppingCartDto
    {
        public Guid Id { get; init; }
        public Guid CustomerId { get; init; }
        public List<ShoppingCartItemDto> Items { get; init; }
        public MoneyValue TotalPrice { get; init; }

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
