using Application.Common.Interfaces;
using Domain.Customers.Entities.Orders.Repositories;
using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.Repositories;
using Domain.Invoices.Repositories;
using Domain.Shops.Entities.ShopOrders;
using Domain.Shops.Entities.ShopOrders.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.CancelOrder
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly ICurrentUserService _userService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IShopOrderRepository _shopOrderRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelOrderCommandHandler(ICurrentUserService userService,
                                       ICustomerRepository customerRepository,
                                       IShopOrderRepository shopOrderRepository,
                                       IInvoiceRepository invoiceRepository,
                                       IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _customerRepository = customerRepository;
            _shopOrderRepository = shopOrderRepository;
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;
            var customer = await _customerRepository.GetCustomerById(customerId);

            customer.CancelOrder(request.Id);

            await CancelRelatedShopOrders(request.Id);

            await CancelAllInvoices(request.Id);

            await _unitOfWork.CommitAsync();
        }

        private async Task CancelRelatedShopOrders(OrderId orderId)
        {
            var shopOrderList = await _shopOrderRepository.GetShopOrdersByOrderId(orderId);

            foreach (var shopOrder in shopOrderList)
            {
                shopOrder.CancelOrder();
            }
        }

        private async Task CancelAllInvoices(OrderId orderId)
        {
            var invoice = await _invoiceRepository.GetInvoiceByOrderId(orderId);

            invoice.CancelInvoice();
        }
    }
}
