using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers
{
    public record CustomerId
    {
        public Guid Value { get;  }

        public CustomerId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyCustomerIdException();
            }

            Value = value;
        }
    }
}
