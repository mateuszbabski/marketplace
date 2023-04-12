using Domain.Customers.Entities.Orders.Exceptions;
using Domain.Customers.Entities.ShoppingCarts.Exceptions;
using Domain.Customers.Entities.ShoppingCarts.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entities.Orders.ValueObjects
{
    public record OrderId
    {
        public Guid Value { get; }

        public OrderId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyOrderIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(OrderId id) => id.Value;
        public static implicit operator OrderId(Guid value) => new(value);
    }
}
