using Domain.Invoices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Invoices.ValueObjects
{
    public record PartialInvoiceId
    {
        public Guid Value { get; }

        public PartialInvoiceId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyPartialInvoiceIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(PartialInvoiceId id) => id.Value;
        public static implicit operator PartialInvoiceId(Guid value) => new(value);
    }
}
