using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Invoices
{
    public enum InvoiceStatus
    {
        Created,
        Sent,
        WaitingForPayment,
        Paid
    }
}
