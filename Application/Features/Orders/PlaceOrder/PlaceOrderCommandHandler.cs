using Application.Common.Interfaces;
using Domain.Customers;
using Domain.Customers.Entities.Orders.Repositories;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using Domain.Customers.Repositories;
using Domain.Shared.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.PlaceOrder
{
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, Guid>
    {
        private readonly ICurrentUserService _userService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public PlaceOrderCommandHandler(ICurrentUserService userService,
                                        ICustomerRepository customerRepository,
                                        IDateTimeProvider dateTimeProvider,
                                        IShoppingCartRepository shoppingCartRepository)
        {
            _userService = userService;
            _customerRepository = customerRepository;
            _dateTimeProvider = dateTimeProvider;
            _shoppingCartRepository = shoppingCartRepository;
        }
        public async Task<Guid> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;
            var customer = await _customerRepository.GetCustomerById(customerId);

            var shoppingCart = customer.GetShoppingCart() ?? throw new Exception("There is no shopping cart available");

            var shippingAddress = CreateShippingAddress(request, customer.Address);

            var order = customer.PlaceOrder(shoppingCart, shippingAddress, _dateTimeProvider.UtcNow);        

            await _shoppingCartRepository.Delete(shoppingCart);

            return order.Id;
        }

        private static Address CreateShippingAddress(PlaceOrderCommand request, Address customerAddress)
        {
            var addressParams = new List<string>()
            {
                request.Country, request.City, request.Street, request.PostalCode
            };

            if (addressParams.All(c => !string.IsNullOrEmpty(c)))
            {
                var newShopAddress = Address.CreateAddress(request.Country, request.City, request.Street, request.PostalCode);
                return newShopAddress;
            }
            else
            {
                return customerAddress;
            }
        }
    }
}
