using Application.Common.Interfaces;
using Application.Features.ShoppingCarts.GetShoppingCartByCustomerId;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var shoppingCartVM = new ShoppingCartDto()
            {
                Id = shoppingCart.Id,
                CustomerId = shoppingCart.CustomerId,
                TotalPrice = shoppingCart.TotalPrice,
                Items = shoppingCart.Items
            };

            return shoppingCartVM;
        }
    }
}
