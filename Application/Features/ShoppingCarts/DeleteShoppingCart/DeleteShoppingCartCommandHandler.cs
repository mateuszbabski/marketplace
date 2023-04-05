using Application.Common.Interfaces;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShoppingCarts.DeleteShoppingCart
{
    public class DeleteShoppingCartCommandHandler : IRequestHandler<DeleteShoppingCartCommand, Unit>
    {
        private readonly ICurrentUserService _userService;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public DeleteShoppingCartCommandHandler(ICurrentUserService userService, IShoppingCartRepository shoppingCartRepository)
        {
            _userService = userService;
            _shoppingCartRepository = shoppingCartRepository;
        }
        public async Task<Unit> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;
            //var shoppingCart = await _shoppingCartRepository.GetShoppingCartById(request.ShoppingCartId)
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByCustomerId(userId)
                ?? throw new Exception("Cart not found.");

            await _shoppingCartRepository.Delete(shoppingCart);

            return Unit.Value;
        }
    }
}
