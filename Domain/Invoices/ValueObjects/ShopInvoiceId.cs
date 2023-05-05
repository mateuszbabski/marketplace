using Domain.Invoices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Invoices.ValueObjects
{
    public record ShopInvoiceId
    {
        public Guid Value { get; }

        public ShopInvoiceId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyPartialInvoiceIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(ShopInvoiceId id) => id.Value;
        public static implicit operator ShopInvoiceId(Guid value) => new(value);
    }
}
