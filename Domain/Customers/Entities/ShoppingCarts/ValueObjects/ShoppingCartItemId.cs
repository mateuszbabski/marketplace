using Domain.Customers.Entities.ShoppingCarts.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entities.ShoppingCarts.ValueObjects
{
    public record ShoppingCartItemId
    {
        public Guid Value { get; }

        public ShoppingCartItemId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyShoppingCartItemIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(ShoppingCartItemId id) => id.Value;
        public static implicit operator ShoppingCartItemId(Guid value) => new(value);
    }
}
