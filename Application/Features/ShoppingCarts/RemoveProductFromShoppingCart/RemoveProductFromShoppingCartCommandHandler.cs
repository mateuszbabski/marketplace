using Application.Common.Interfaces;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShoppingCarts.RemoveProductFromShoppingCart
{
    public class RemoveProductFromShoppingCartCommandHandler : IRequestHandler<RemoveProductFromShoppingCartCommand, Unit>
    {
        private readonly ICurrentUserService _userService;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public RemoveProductFromShoppingCartCommandHandler(ICurrentUserService userService, IShoppingCartRepository shoppingCartRepository)
        {
            _userService = userService;
            _shoppingCartRepository = shoppingCartRepository;
        }
        public async Task<Unit> Handle(RemoveProductFromShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByCustomerId(customerId);

            if (shoppingCart == null)
            {
                throw new Exception("Cart not found.");
            }

            shoppingCart.RemoveItemFromCart(request.Id);

            await _shoppingCartRepository.RemoveItem(shoppingCart, request.Id);

            return Unit.Value;
        }
    }
}
