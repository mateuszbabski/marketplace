using Domain.Shared.Abstractions;

namespace Domain.Customers.Entities.ShoppingCarts.Events
{
    public sealed record ProductQuantityChangedDomainEvent(ShoppingCart ShoppingCart,
                                                           ShoppingCartItem ShoppingCartItem) 
        : IDomainEvent
    {
    }
}
