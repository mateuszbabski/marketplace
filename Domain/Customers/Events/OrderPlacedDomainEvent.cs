using Domain.Customers.Entities.Orders;
using Domain.Shared.Abstractions;

namespace Domain.Customers.Events
{
    public sealed record OrderPlacedDomainEvent(Order Order) : IDomainEvent
    {
    }
}
