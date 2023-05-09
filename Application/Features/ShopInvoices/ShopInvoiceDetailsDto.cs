using Application.Features.Invoices;
using Domain.Invoices;
using Domain.Invoices.ValueObjects;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.ShopOrders.ValueObjects;
using Domain.Shops.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShopInvoices
{
    public record ShopInvoiceDetailsDto
    {
        public Guid Id { get; init; }
        public Guid InvoiceId { get; init; }
        public Guid ShopId { get; init; }
        public MoneyValue PartialOrderPrice { get; init; }
        public Guid ShopOrderId { get; init; }
        public DateTime CreatedOn { get; init; }
        public string InvoiceStatus { get; init; }
        public DateTime? DateOfPayment { get; init; }

        public static ShopInvoiceDetailsDto CreateShopInvoiceDetailsDtoFromObject(ShopInvoice shopInvoice)
        {
            return new ShopInvoiceDetailsDto()
            {
                Id = shopInvoice.Id,
                InvoiceId = shopInvoice.InvoiceId,
                ShopId = shopInvoice.ShopId,
                PartialOrderPrice = shopInvoice.PartialOrderPrice,
                ShopOrderId = shopInvoice.ShopOrderId,
                CreatedOn = shopInvoice.CreatedOn,
                InvoiceStatus = shopInvoice.InvoiceStatus.ToString(),
                DateOfPayment = shopInvoice.DateOfPayment,
            };
        }
    }
}
