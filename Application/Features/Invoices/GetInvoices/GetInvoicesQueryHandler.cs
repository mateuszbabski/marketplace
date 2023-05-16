using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Invoices.Repositories;
using MediatR;

namespace Application.Features.Invoices.GetInvoices
{
    public class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, IEnumerable<InvoiceDto>>
    {
        private readonly ICurrentUserService _userService;
        private readonly IInvoiceRepository _invoiceRepository;

        public GetInvoicesQueryHandler(ICurrentUserService userService, IInvoiceRepository invoiceRepository)
        {
            _userService = userService;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<IEnumerable<InvoiceDto>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;

            var invoiceList = await _invoiceRepository.GetAllInvoicesForCustomer(userId)
                ?? throw new NotFoundException("There is no available invoices");

            var invoiceListDto = InvoiceDto.CreateInvoiceDtoFromObject(invoiceList);

            return invoiceListDto;
        }
    }
}
