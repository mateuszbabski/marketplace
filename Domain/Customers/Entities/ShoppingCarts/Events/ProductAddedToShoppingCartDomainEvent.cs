using Domain.Shared.Abstractions;

namespace Domain.Customers.Entities.ShoppingCarts.Events
{
    public sealed record ProductAddedToShoppingCartDomainEvent(ShoppingCart ShoppingCart,
                                                               ShoppingCartItem ShoppingCartItem) 
        : IDomainEvent
    {
    }
}
