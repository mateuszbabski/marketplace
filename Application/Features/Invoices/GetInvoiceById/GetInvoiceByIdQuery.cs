using MediatR;

namespace Application.Features.Invoices.GetInvoiceById
{
    public class GetInvoiceByIdQuery : IRequest<InvoiceDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
