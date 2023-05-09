using Domain.Invoices;
using Domain.Invoices.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Domain.Invoices.ValueObjects;
using Domain.Customers.ValueObjects;
using Domain.Shops.ValueObjects;
using Domain.Customers;
using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Shops;

namespace Infrastructure.Repositories
{
    internal sealed class InvoiceRepository : IInvoiceRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public InvoiceRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Invoice> Add(Invoice invoice)
        {
            await _dbContext.Invoices.AddAsync(invoice);

            return invoice;
        }

        public async Task<Invoice> GetByIdForCustomer(InvoiceId id, CustomerId customerId)
        {
            return await _dbContext.Invoices.Where(x => x.CustomerId == customerId)
                                            .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<Invoice> GetInvoiceByOrderId(OrderId id)
        {
            return await _dbContext.Invoices.Where(x => x.OrderId == id)
                                            .Include(x => x.ShopInvoices)
                                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesForCustomer(CustomerId customerId)
        {
            return await _dbContext.Invoices.Where(x => x.CustomerId == customerId)
                                            .ToListAsync();
        }

        public async Task<ShopInvoice> GetByIdForShop(ShopInvoiceId id, ShopId shopId)
        {
            return await _dbContext.ShopInvoices.Where(x => x.ShopId == shopId)
                                                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<ShopInvoice>> GetAllInvoicesForShop(ShopId shopId)
        {
            return await _dbContext.ShopInvoices.Where(x => x.ShopId == shopId)
                                                .ToListAsync();
        }        
    }
}
