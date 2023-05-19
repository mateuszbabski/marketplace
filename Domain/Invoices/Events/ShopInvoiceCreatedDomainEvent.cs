using Domain.Shared.Abstractions;

namespace Domain.Invoices.Events
{
    public sealed record ShopInvoiceCreatedDomainEvent(ShopInvoice ShopInvoice) 
        : IDomainEvent
    {
    }
}
