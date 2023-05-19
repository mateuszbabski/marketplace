using Application.Common.Interfaces;
using Domain.Customers.Entities.Orders.Events;
using Domain.Customers.Events;
using Domain.Invoices;
using Domain.Invoices.Repositories;
using MediatR;
using Serilog;

namespace Application.Features.Orders.PlaceOrder
{
    internal sealed class OrderPlacedDomainEventHandler
        : INotificationHandler<OrderPlacedDomainEvent>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IInvoiceRepository _invoiceRepository;

        public OrderPlacedDomainEventHandler(IDateTimeProvider dateTimeProvider, IInvoiceRepository invoiceRepository)
        {
            _dateTimeProvider = dateTimeProvider;
            _invoiceRepository = invoiceRepository;
        }
        public async Task Handle(OrderPlacedDomainEvent notification, CancellationToken cancellationToken)
        {
            Log.Information("Order Placed Domain Event - Creating Invoices");
            var invoice = Invoice.CreateInvoice(notification.Order, _dateTimeProvider.UtcNow);

            await _invoiceRepository.Add(invoice);

            // create payment(invoice);
        }
    }
}
