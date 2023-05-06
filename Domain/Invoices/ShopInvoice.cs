using Domain.Invoices.ValueObjects;
using Domain.Shared.Abstractions;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.ShopOrders.ValueObjects;
using Domain.Shops.ValueObjects;

namespace Domain.Invoices
{
    public class ShopInvoice : Entity
    {
        public ShopInvoiceId Id { get; private set; }
        public InvoiceId InvoiceId { get; private set; }
        public ShopId ShopId { get; private set; }
        public MoneyValue PartialOrderPrice { get; private set; }
        public ShopOrderId ShopOrderId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public InvoiceStatus InvoiceStatus { get; private set; } = InvoiceStatus.Created;
        public DateTime? DateOfPayment { get; private set; } = null;

        //private ShopInvoice() { }
        private ShopInvoice() 
        {
            Id = new ShopInvoiceId(Guid.NewGuid());
        }
        internal static ShopInvoice CreateShopInvoice()
        {
            return new ShopInvoice();
        }
    }
}
