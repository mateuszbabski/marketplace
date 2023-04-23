using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Invoices.Exceptions
{
    public class EmptyPartialInvoiceIdException : Exception
    {
        public EmptyPartialInvoiceIdException() : base(message: "PartialInvoiceId can not be empty.")
        {
        }
    }
}
