using Application.Common.Interfaces;
using Domain.Customers;
using Domain.Customers.Entities.Orders.Repositories;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using Domain.Customers.Repositories;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.ShopOrders;
using Domain.Shops.Entities.ShopOrders.Repositories;
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
        private readonly IShopOrderRepository _shopOrderRepository;

        public PlaceOrderCommandHandler(ICurrentUserService userService,
                                        ICustomerRepository customerRepository,
                                        IDateTimeProvider dateTimeProvider,
                                        IShoppingCartRepository shoppingCartRepository,
                                        IShopOrderRepository shopOrderRepository)
        {
            _userService = userService;
            _customerRepository = customerRepository;
            _dateTimeProvider = dateTimeProvider;
            _shoppingCartRepository = shoppingCartRepository;
            _shopOrderRepository = shopOrderRepository;
        }
        public async Task<Guid> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;
            var customer = await _customerRepository.GetCustomerById(customerId);

            var shoppingCart = customer.GetShoppingCart() ?? throw new Exception("There is no shopping cart available");

            var shippingAddress = CreateShippingAddress(request, customer.Address);

            var order = customer.PlaceOrder(shoppingCart, shippingAddress, _dateTimeProvider.UtcNow);

            // extract to other method
            var productList = new List<ShoppingCartItem>();
            foreach (var productByShopList in shoppingCart.Items.GroupBy(x => x.ShopId))
            {
                var productsToAdd = productByShopList.ToList();
                productList.AddRange(productsToAdd);
                var shopOrder = ShopOrder.CreateShopOrder(order, productList);
                await _shopOrderRepository.Add(shopOrder);
            }

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
