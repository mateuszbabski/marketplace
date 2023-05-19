using Domain.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entities.ShoppingCarts.Events
{
    public sealed record ShoppingCartCurrencyChangedDomainEvent(ShoppingCart ShoppingCart) : IDomainEvent
    {
    }
}
