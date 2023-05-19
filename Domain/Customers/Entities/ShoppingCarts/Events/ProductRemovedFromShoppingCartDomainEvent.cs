using Domain.Shared.Abstractions;

namespace Domain.Customers.Entities.ShoppingCarts.Events
{
    public sealed record ProductRemovedFromShoppingCartDomainEvent(ShoppingCart ShoppingCart,
                                                                   ShoppingCartItem ShoppingCartItem) 
        : IDomainEvent
    {
    }
}
