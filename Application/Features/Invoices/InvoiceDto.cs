using Domain.Invoices;
using Domain.Shared.ValueObjects;

namespace Application.Features.Invoices
{
    public record InvoiceDto
    {
        public Guid Id { get; init; }
        public Guid OrderId { get; init; }
        public MoneyValue TotalPrice { get; init; }
        public DateTime CreatedOn { get; init; }
        public string InvoiceStatus { get; init; }

        public static IEnumerable<InvoiceDto> CreateInvoiceDtoFromObject(IEnumerable<Invoice> invoices)
        {
            var invoiceList = new List<InvoiceDto>();

            foreach (var invoice in invoices)
            {
                var invoiceDto = new InvoiceDto()
                {
                    Id = invoice.Id,
                    OrderId = invoice.OrderId,
                    TotalPrice = invoice.TotalPrice,
                    CreatedOn = invoice.CreatedOn,
                    InvoiceStatus = invoice.InvoiceStatus.ToString()
                };

                invoiceList.Add(invoiceDto);
            }
            return invoiceList;
        }
    }
}
