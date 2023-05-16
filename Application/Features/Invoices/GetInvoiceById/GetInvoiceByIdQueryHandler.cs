using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Invoices.Repositories;
using MediatR;

namespace Application.Features.Invoices.GetInvoiceById
{
    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceDetailsDto>
    {
        private readonly ICurrentUserService _userService;
        private readonly IInvoiceRepository _invoiceRepository;

        public GetInvoiceByIdQueryHandler(ICurrentUserService userService, IInvoiceRepository invoiceRepository)
        {
            _userService = userService;
            _invoiceRepository = invoiceRepository;
        }
        public async Task<InvoiceDetailsDto> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;

            var invoice = await _invoiceRepository.GetByIdForCustomer(request.Id, userId)
                ?? throw new NotFoundException("There is no available invoice with current id");

            var invoiceDto = InvoiceDetailsDto.CreateInvoiceDetailsDtoFromObject(invoice);

            return invoiceDto;
        }
    }
}
