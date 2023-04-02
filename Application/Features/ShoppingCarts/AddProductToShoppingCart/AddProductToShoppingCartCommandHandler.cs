﻿using Application.Common.Interfaces;
using Domain.Customers;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using Domain.Customers.ValueObjects;
using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.Products.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShoppingCarts.AddProductToShoppingCart
{
    public class AddProductToShoppingCartCommandHandler : IRequestHandler<AddProductToShoppingCartCommand, Guid>
    {
        private readonly ICurrentUserService _userService;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;

        public AddProductToShoppingCartCommandHandler(ICurrentUserService userService,
                                                      IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
        {
            _userService = userService;
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(AddProductToShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;
            var product = await _productRepository.GetById(request.ProductId);
            var shoppingCart = await ReturnOrCreateNewShoppingCart(customerId);

            shoppingCart.AddProductToShoppingCart(request.ProductId, request.Quantity, product.GetPrice());

            await _shoppingCartRepository.Update(shoppingCart);            

            return shoppingCart.Id;
        }

        private async Task<ShoppingCart> ReturnOrCreateNewShoppingCart(Guid customerId)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByCustomerId(customerId);

            if (shoppingCart == null)
            {
                var newShoppingCart = ShoppingCart.CreateShoppingCart(Guid.NewGuid(), customerId);
                await _shoppingCartRepository.Create(newShoppingCart);
                return newShoppingCart;
            }

            return shoppingCart;
        }
    }
}
