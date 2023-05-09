using Application.Common.Interfaces;
using Domain.Invoices.Repositories;
using MediatR;

namespace Application.Features.ShopInvoices.GetShopInvoices
{
    public class GetShopInvoicesQueryHandler : IRequestHandler<GetShopInvoicesQuery, IEnumerable<ShopInvoiceDto>>
    {
        private readonly ICurrentUserService _userService;
        private readonly IInvoiceRepository _invoiceRepository;

        public GetShopInvoicesQueryHandler(ICurrentUserService userService, IInvoiceRepository invoiceRepository)
        {
            _userService = userService;
            _invoiceRepository = invoiceRepository;
        }
        public async Task<IEnumerable<ShopInvoiceDto>> Handle(GetShopInvoicesQuery request, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;

            var shopInvoices = await _invoiceRepository.GetAllInvoicesForShop(userId);

            var shopInvoicesDto = ShopInvoiceDto.CreateShopInvoiceDtoFromObject(shopInvoices);

            return shopInvoicesDto;
        }
    }
}
