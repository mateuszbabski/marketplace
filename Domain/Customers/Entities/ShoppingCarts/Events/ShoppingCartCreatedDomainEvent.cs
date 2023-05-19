using Domain.Shared.Abstractions;

namespace Domain.Customers.Entities.ShoppingCarts.Events
{
    public sealed record ShoppingCartCreatedDomainEvent(ShoppingCart ShoppingCart) : IDomainEvent
    {
    }
}
