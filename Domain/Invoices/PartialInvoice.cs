using Domain.Customers.ValueObjects;
using Domain.Invoices.ValueObjects;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.ShopOrders.ValueObjects;
using Domain.Shops.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Invoices
{
    public class PartialInvoice
    {
        public PartialInvoiceId Id { get; private set; }
        public InvoiceId InvoiceId { get; private set; }
        public ShopId ShopId { get; private set; }
        public MoneyValue PartialOrderPrice { get; private set; }
        public ShopOrderId ShopOrderId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public InvoiceStatus InvoiceStatus { get; private set; } = InvoiceStatus.Created;
        public DateTime? DateOfPayment { get; private set; } = null;

        //private PartialInvoice() { }
        private PartialInvoice() 
        {
            Id = Guid.NewGuid();
        }
        internal static PartialInvoice CreatePartialInvoice()
        {
            return new PartialInvoice();
        }
    }
}
