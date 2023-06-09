﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using Domain.Shared.Rules;
using Domain.Shops.Entities.Products.Exceptions;
using MediatR;

namespace Application.Features.ShoppingCarts.ChangeShoppingCartCurrency
{
    public class ChangeShoppingCartCurrencyCommandHandler : IRequestHandler<ChangeShoppingCartCurrencyCommand>
    {
        private readonly ICurrentUserService _userService;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICurrencyConverter _currencyConverter;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeShoppingCartCurrencyCommandHandler(ICurrentUserService userService,
                                                        IShoppingCartRepository shoppingCartRepository,
                                                        ICurrencyConverter currencyConverter,
                                                        IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _shoppingCartRepository = shoppingCartRepository;
            _currencyConverter = currencyConverter;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(ChangeShoppingCartCurrencyCommand request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByCustomerId(customerId)
                ?? throw new NotFoundException("Cart not found.");

            // find better solution to avoid mistakes and errors with currency
            if(request.Currency.ToUpper() == shoppingCart.TotalPrice.Currency
                || new SystemMustAcceptsCurrencyRule(request.Currency).IsBroken())
            {
                throw new InvalidProductPriceException("Invalid currency.");
            }

            var conversionRate = await _currencyConverter.GetConversionRate(shoppingCart.TotalPrice.Currency,
                                                                            request.Currency);

            shoppingCart.ChangeShoppingCartCurrency(conversionRate, request.Currency);

            await _unitOfWork.CommitAsync();
        }
    }
}
