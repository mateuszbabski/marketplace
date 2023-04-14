using Domain.Customers.Entities.Orders;
using Domain.Customers.Entities.Orders.Repositories;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    internal sealed class OrderRepository : IOrderRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public OrderRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> Add(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task Update(Order order)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
