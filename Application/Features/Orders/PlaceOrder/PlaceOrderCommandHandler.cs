using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using Domain.Customers.Repositories;
using Domain.Invoices;
using Domain.Invoices.Repositories;
using Domain.Shared.ValueObjects;
using MediatR;

namespace Application.Features.Orders.PlaceOrder
{
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, Guid>
    {
        private readonly ICurrentUserService _userService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PlaceOrderCommandHandler(ICurrentUserService userService,
                                        ICustomerRepository customerRepository,
                                        IDateTimeProvider dateTimeProvider,
                                        IShoppingCartRepository shoppingCartRepository,
                                        IInvoiceRepository invoiceRepository,
                                        IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _customerRepository = customerRepository;
            _dateTimeProvider = dateTimeProvider;
            _shoppingCartRepository = shoppingCartRepository;
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;
            var customer = await _customerRepository.GetCustomerWithCartById(customerId);

            var shoppingCart = customer.GetShoppingCart() 
                ?? throw new NotFoundException("There is no shopping cart available");

            var shippingAddress = CreateShippingAddress(request, customer.Address);

            var order = customer.PlaceOrder(shoppingCart, shippingAddress, _dateTimeProvider.UtcNow);

            var invoice = Invoice.CreateInvoice(order, _dateTimeProvider.UtcNow);

            _shoppingCartRepository.DeleteCart(shoppingCart);

            await _invoiceRepository.Add(invoice);

            await _unitOfWork.CommitAsync();
            
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
            
            return customerAddress;
        }
    }
}
