using Domain.Customers.ValueObjects;
using Domain.Invoices.ValueObjects;
using Domain.Shops.ValueObjects;

namespace Domain.Invoices.Repositories
{
    public interface IInvoiceRepository
    {       
        Task<Invoice> Add(Invoice invoice);
        Task<Invoice> GetByIdForCustomer(InvoiceId id, CustomerId customerId);
        Task<IEnumerable<Invoice>> GetAllInvoicesForCustomer(CustomerId customerId);
        Task<ShopInvoice> Add(ShopInvoice shopInvoice);
        Task<ShopInvoice> GetByIdForShop(ShopInvoiceId id, ShopId shopId);
        Task<IEnumerable<ShopInvoice>> GetAllInvoicesForShop(ShopId shopId);
    }
}
