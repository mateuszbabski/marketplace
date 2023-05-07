using Domain.Customers.Entities.Orders;
using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.ValueObjects;
using Domain.Invoices.ValueObjects;
using Domain.Shared.Abstractions;
using Domain.Shared.ValueObjects;

namespace Domain.Invoices
{
    public class Invoice : Entity, IAggregateRoot
    {
        public InvoiceId Id { get; private set; }
        public CustomerId CustomerId { get; private set; }
        public OrderId OrderId { get; private set; }
        public MoneyValue TotalPrice { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public InvoiceStatus InvoiceStatus { get; private set; } = InvoiceStatus.Created;
        public DateTime? DateOfPayment { get; private set; } = null;
        public List<ShopInvoice> ShopInvoices { get; private set; }

        private Invoice() { }

        private Invoice(Order order, DateTime createdOn)
        {
            Id = new InvoiceId(Guid.NewGuid());
            CustomerId = order.CustomerId;
            OrderId = order.Id;
            TotalPrice = order.TotalPrice;
            CreatedOn = createdOn;

            ShopInvoices = new List<ShopInvoice>();
        }

        public static Invoice CreateInvoice(Order order, DateTime createdOn)
        {
            var invoice = new Invoice(order, createdOn);

            foreach (var shopOrder in order.ShopOrders)
            {
                var shopInvoice = ShopInvoice.CreateShopInvoice(invoice.Id, shopOrder, createdOn);
                invoice.ShopInvoices.Add(shopInvoice);
            }

            return invoice;
        }
    }
}
