using Domain.Customers.Entities.ShoppingCarts.Exceptions;
using Domain.Shops.Exceptions;
using Domain.Shops.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entities.ShoppingCarts.ValueObjects
{
    public record ShoppingCartId
    {
        public Guid Value { get; }

        public ShoppingCartId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyShoppingCartIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(ShoppingCartId id) => id.Value;
        public static implicit operator ShoppingCartId(Guid value) => new(value);
    }
}
