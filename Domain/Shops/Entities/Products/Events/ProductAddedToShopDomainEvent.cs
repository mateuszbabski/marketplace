using Domain.Shared.Abstractions;

namespace Domain.Shops.Entities.Products.Events
{
    public sealed record ProductAddedToShopDomainEvent(Product Product) : IDomainEvent
    {
    }
}
