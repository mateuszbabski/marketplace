using Domain.Invoices.ValueObjects;
using Domain.Shared.Abstractions;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.ShopOrders;
using Domain.Shops.Entities.ShopOrders.ValueObjects;
using Domain.Shops.ValueObjects;
using System.Text.Json.Serialization;

namespace Domain.Invoices
{
    public class ShopInvoice : Entity
    {
        public ShopInvoiceId Id { get; private set; }
        public InvoiceId InvoiceId { get; private set; }
        public ShopId ShopId { get; private set; }
        public MoneyValue PartialOrderPrice { get; private set; }
        public ShopOrderId ShopOrderId { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public InvoiceStatus InvoiceStatus { get; private set; } = InvoiceStatus.Created;
        public DateTime? DateOfPayment { get; private set; } = null;
        [JsonIgnore]
        public virtual Invoice Invoice { get; private set; }

        private ShopInvoice() { }
        private ShopInvoice(InvoiceId invoiceId, ShopOrder shopOrder, DateTime createdOn) 
        {
            Id = new ShopInvoiceId(Guid.NewGuid());
            InvoiceId = invoiceId;
            ShopId = shopOrder.ShopId;
            ShopOrderId = shopOrder.Id;
            PartialOrderPrice = shopOrder.TotalPrice;
            CreatedOn = createdOn;
        }
        internal static ShopInvoice CreateShopInvoice(InvoiceId invoiceId, ShopOrder shopOrder, DateTime createdOn)
        {
            return new ShopInvoice(invoiceId, shopOrder, createdOn);
        }
    }
}
