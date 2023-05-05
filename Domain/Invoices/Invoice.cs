using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.ValueObjects;
using Domain.Invoices.ValueObjects;
using Domain.Shared.Abstractions;
using Domain.Shared.ValueObjects;

namespace Domain.Invoices
{
    public class Invoice : IAggregateRoot
    {
        public InvoiceId Id { get; private set; }
        public CustomerId CustomerId { get; private set; }
        public OrderId OrderId { get; private set; }
        public MoneyValue TotalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public InvoiceStatus InvoiceStatus { get; private set; } = InvoiceStatus.Created;
        public DateTime? DateOfPayment { get; private set; } = null;
        public List<ShopInvoice> ShopInvoices { get; private set; }

        //private Invoice() { }

        private Invoice()
        {
            Id = new InvoiceId(Guid.NewGuid());
            ShopInvoices = new List<ShopInvoice>();
        }

        public static Invoice CreateInvoice()
        {
            return new Invoice();
        }
    }
}
