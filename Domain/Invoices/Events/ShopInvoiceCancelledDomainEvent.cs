using Domain.Shared.Abstractions;

namespace Domain.Invoices.Events
{
    public sealed record ShopInvoiceCancelledDomainEvent(ShopInvoice ShopInvoice) 
        : IDomainEvent
    {
    }
}
