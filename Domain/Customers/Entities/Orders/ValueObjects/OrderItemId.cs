using Domain.Customers.Entities.Orders.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entities.Orders.ValueObjects
{
    public record OrderItemId
    {
        public Guid Value { get; }

        public OrderItemId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyOrderItemIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(OrderItemId id) => id.Value;
        public static implicit operator OrderItemId(Guid value) => new(value);
    }
}
