using Domain.Customers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.ValueObject
{
    public record CustomerId
    {
        public Guid Value { get; }

        public CustomerId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyCustomerIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(CustomerId id) => id.Value;
        public static implicit operator CustomerId(Guid value) => new(value);
    }
}
