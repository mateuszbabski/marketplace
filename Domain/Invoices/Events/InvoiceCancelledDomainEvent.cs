using Domain.Shared.Abstractions;

namespace Domain.Invoices.Events
{
    public sealed record InvoiceCancelledDomainEvent(Invoice Invoice) : IDomainEvent
    {
    }
}
