using Domain.Customers.Entities.Orders.Exceptions;
using Domain.Invoices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Invoices.ValueObjects
{
    public record InvoiceId
    {
        public Guid Value { get; }

        public InvoiceId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyInvoiceIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(InvoiceId id) => id.Value;
        public static implicit operator InvoiceId(Guid value) => new(value);
    }
}
