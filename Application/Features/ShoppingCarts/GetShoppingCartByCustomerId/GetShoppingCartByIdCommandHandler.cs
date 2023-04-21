using Application.Common.Interfaces;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using MediatR;

namespace Application.Features.ShoppingCarts.GetShoppingCartByCustomerId
{
    public class GetShoppingCartByCustomerIdCommandHandler : IRequestHandler<GetShoppingCartByCustomerIdCommand, ShoppingCartDto>
    {
        private readonly ICurrentUserService _userService;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public GetShoppingCartByCustomerIdCommandHandler(ICurrentUserService userService, IShoppingCartRepository shoppingCartRepository)
        {
            _userService = userService;
            _shoppingCartRepository = shoppingCartRepository;
        }
        public async Task<ShoppingCartDto> Handle(GetShoppingCartByCustomerIdCommand request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByCustomerId(customerId);

            if (shoppingCart == null)
            {
                throw new Exception("Cart not found.");
            }

            var shoppingCartItemsList = new List<ShoppingCartItemDto>();

            foreach (var item in shoppingCart.Items)
            {
                var itemDto = new ShoppingCartItemDto()
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                };

                shoppingCartItemsList.Add(itemDto);
            }

            var shoppingCartDto = new ShoppingCartDto()
            {
                Id = shoppingCart.Id,
                CustomerId = shoppingCart.CustomerId,
                TotalPrice = shoppingCart.TotalPrice,
                Items = shoppingCartItemsList
            };

            return shoppingCartDto;
        }
    }
}
