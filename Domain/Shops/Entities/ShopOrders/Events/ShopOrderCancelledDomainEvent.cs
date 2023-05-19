﻿using Domain.Shared.Abstractions;

namespace Domain.Shops.Entities.ShopOrders.Events
{
    public sealed record ShopOrderCancelledDomainEvent(ShopOrder ShopOrder) : IDomainEvent
    {
    }
}
