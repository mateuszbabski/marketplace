using Application.Common.Exceptions;
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
    public class DeleteShoppingCartCommandHandler : IRequestHandler<DeleteShoppingCartCommand>
    {
        private readonly ICurrentUserService _userService;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteShoppingCartCommandHandler(ICurrentUserService userService,
                                                IShoppingCartRepository shoppingCartRepository,
                                                IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _shoppingCartRepository = shoppingCartRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;

            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByCustomerId(userId)
                ?? throw new NotFoundException("Cart not found.");

            _shoppingCartRepository.DeleteCart(shoppingCart);

            await _unitOfWork.CommitAsync();
        }
    }
}
