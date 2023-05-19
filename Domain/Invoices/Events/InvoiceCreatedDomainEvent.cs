using Domain.Shared.Abstractions;

namespace Domain.Invoices.Events
{
    public sealed record InvoiceCreatedDomainEvent(Invoice Invoice) : IDomainEvent
    {
    }
}
