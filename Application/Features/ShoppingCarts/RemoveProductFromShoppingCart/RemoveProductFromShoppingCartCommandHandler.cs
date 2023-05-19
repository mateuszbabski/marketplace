using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using MediatR;
using Serilog;

namespace Application.Features.ShoppingCarts.RemoveProductFromShoppingCart
{
    public class RemoveProductFromShoppingCartCommandHandler : IRequestHandler<RemoveProductFromShoppingCartCommand>
    {
        private readonly ICurrentUserService _userService;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveProductFromShoppingCartCommandHandler(ICurrentUserService userService,
                                                           IShoppingCartRepository shoppingCartRepository,
                                                           IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _shoppingCartRepository = shoppingCartRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(RemoveProductFromShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByCustomerId(customerId) 
                ?? throw new NotFoundException("Cart not found.");

            shoppingCart.RemoveItemFromCart(request.Id);

            if (shoppingCart.Items.Count == 0)
            {
                _shoppingCartRepository.DeleteCart(shoppingCart);
            }

            await _unitOfWork.CommitAsync();
        }
    }
}
