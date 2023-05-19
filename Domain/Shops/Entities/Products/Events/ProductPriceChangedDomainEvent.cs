using Domain.Shared.Abstractions;

namespace Domain.Shops.Entities.Products.Events
{
    public sealed record ProductPriceChangedDomainEvent(Product Product) : IDomainEvent
    {
    }
}
