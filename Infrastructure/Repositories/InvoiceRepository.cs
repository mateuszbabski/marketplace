//using Domain.Invoices;
//using Domain.Invoices.Repositories;
//using Infrastructure.Context;
//using Microsoft.EntityFrameworkCore;
//using Domain.Invoices.ValueObjects;
//using Domain.Customers.ValueObjects;

//namespace Infrastructure.Repositories
//{
//    internal sealed class InvoiceRepository : IInvoiceRepository
//    {
//        private readonly IApplicationDbContext _dbContext;

//        public InvoiceRepository(IApplicationDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task<Invoice> Add(Invoice invoice)
//        {
//            await _dbContext.Invoices.AddAsync(invoice);
//            await _dbContext.SaveChangesAsync();

//            return invoice;
//        }

//        public async Task<Invoice> GetByIdForCustomer(InvoiceId id, CustomerId customerId)
//        {
//            return await _dbContext.Invoices.Where(x => x.CustomerId == customerId)
//                                            .FirstOrDefaultAsync(e => e.Id == id);
//        }

//        public async Task<IEnumerable<Invoice>> GetAllInvoicesForCustomer(CustomerId customerId)
//        {
//            return await _dbContext.Invoices.Where(x => x.CustomerId == customerId)
//                                            .ToListAsync();
//        }
//    }
//}
