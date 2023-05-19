using Domain.Shared.Abstractions;

namespace Domain.Invoices.Events
{
    public sealed record InvoiceStatusChangedDomainEvent(Invoice Invoice) : IDomainEvent
    {
    }
}
