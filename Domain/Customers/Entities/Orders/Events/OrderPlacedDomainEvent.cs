using Domain.Shared.Abstractions;

namespace Domain.Customers.Entities.Orders.Events
{
    public sealed record OrderPlacedDomainEvent(Order Order) : IDomainEvent
    { }
}
