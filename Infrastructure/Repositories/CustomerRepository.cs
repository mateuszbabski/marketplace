using Domain.Customers;
using Domain.Customers.Repositories;
using Domain.Customers.ValueObjects;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal sealed class CustomerRepository : ICustomerRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public CustomerRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Customer> Add(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Customer> GetCustomerById(CustomerId id)
        {
            return await _dbContext.Customers.SingleAsync(c => c.Id == id);
        }
    }
}
