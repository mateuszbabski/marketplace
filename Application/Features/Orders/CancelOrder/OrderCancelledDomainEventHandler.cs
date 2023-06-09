﻿using Domain.Customers.Entities.Orders.Events;
using Domain.Invoices.Repositories;
using Domain.Shops.Entities.ShopOrders.Repositories;
using MediatR;
using Serilog;

namespace Application.Features.Orders.CancelOrder
{
    internal sealed class OrderCancelledDomainEventHandler : INotificationHandler<OrderCancelledDomainEvent>
    {
        private readonly IShopOrderRepository _shopOrderRepository;
        private readonly IInvoiceRepository _invoiceRepository;

        public OrderCancelledDomainEventHandler(IShopOrderRepository shopOrderRepository,
                                                IInvoiceRepository invoiceRepository)
        {
            _shopOrderRepository = shopOrderRepository;
            _invoiceRepository = invoiceRepository;
        }
        public async Task Handle(OrderCancelledDomainEvent notification, CancellationToken cancellationToken)
        {
            var shopOrderList = await _shopOrderRepository.GetShopOrdersByOrderId(notification.OrderId);
            
            foreach (var shopOrder in shopOrderList)
            {
                shopOrder.CancelOrder();
            }

            var invoice = await _invoiceRepository.GetInvoiceByOrderId(notification.OrderId);

            invoice.CancelInvoice();
        }
    }
}
