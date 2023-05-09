using Application.Common.Interfaces;
using Domain.Invoices.Repositories;
using MediatR;

namespace Application.Features.ShopInvoices.GetShopInvoiceById
{
    public class GetShopInvoiceByIdQueryHandler : IRequestHandler<GetShopInvoiceByIdQuery, ShopInvoiceDetailsDto>
    {
        private readonly ICurrentUserService _userService;
        private readonly IInvoiceRepository _invoiceRepository;

        public GetShopInvoiceByIdQueryHandler(ICurrentUserService userService, IInvoiceRepository invoiceRepository)
        {
            _userService = userService;
            _invoiceRepository = invoiceRepository;
        }
        public async Task<ShopInvoiceDetailsDto> Handle(GetShopInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;

            var shopInvoice = await _invoiceRepository.GetByIdForShop(request.Id, userId);

            var shopInvoiceDto = ShopInvoiceDetailsDto.CreateShopInvoiceDetailsDtoFromObject(shopInvoice);

            return shopInvoiceDto;
        }
    }
}
