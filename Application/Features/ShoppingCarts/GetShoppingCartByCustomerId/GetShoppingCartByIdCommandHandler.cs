using Application.Common.Exceptions;
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
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByCustomerId(customerId) 
                ?? throw new NotFoundException("Cart not found.");

            var shoppingCartDto = ShoppingCartDto.CreateShoppingCartDtoFromObject(shoppingCart);

            return shoppingCartDto;
        }
    }
}
