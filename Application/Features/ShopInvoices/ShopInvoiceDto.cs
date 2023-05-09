using Domain.Invoices;
using Domain.Shared.ValueObjects;

namespace Application.Features.ShopInvoices
{
    public record ShopInvoiceDto
    {
        public Guid Id { get; init; }
        public Guid ShopOrderId { get; init; }
        public MoneyValue TotalPrice { get; init; }
        public DateTime CreatedOn { get; init; }
        public string InvoiceStatus { get; init; }

        public static IEnumerable<ShopInvoiceDto> CreateShopInvoiceDtoFromObject(IEnumerable<ShopInvoice> shopInvoices)
        {
            var shopInvoiceList = new List<ShopInvoiceDto>();

            foreach (var shopInvoice in shopInvoices)
            {
                var shopInvoiceDto = new ShopInvoiceDto()
                {
                    Id = shopInvoice.Id,
                    ShopOrderId = shopInvoice.ShopOrderId,
                    TotalPrice = shopInvoice.PartialOrderPrice,
                    CreatedOn = shopInvoice.CreatedOn,
                    InvoiceStatus = shopInvoice.InvoiceStatus.ToString()
                };

                shopInvoiceList.Add(shopInvoiceDto);
            }
            return shopInvoiceList;
        }
    }
}
