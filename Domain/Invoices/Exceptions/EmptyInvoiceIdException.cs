using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Invoices.Exceptions
{
    public class EmptyInvoiceIdException : Exception
    {
        public EmptyInvoiceIdException() : base(message: "InvoiceId can not be empty.")
        {
        }
    }
}
