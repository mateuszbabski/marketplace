using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Invoices.GetInvoices
{
    public class GetInvoicesQuery : IRequest<IEnumerable<InvoiceDto>>
    {
    }
}
