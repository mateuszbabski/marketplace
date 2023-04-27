using Domain.Customers.Entities.Orders;
using Domain.Customers.Entities.Orders.Repositories;
using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.ValueObjects;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Order> GetOrderById(OrderId Id, CustomerId customerId)
        {
            return await _dbContext.Orders.Where(o => o.CustomerId == customerId)
                                          .Include(o => o.OrderItems)
                                          .Include(o => o.ShopOrders)
                                          .FirstOrDefaultAsync(o => o.Id == Id);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersForCustomer(CustomerId customerId)
        {
            return await _dbContext.Orders.Where(o => o.CustomerId == customerId)
                                          .ToListAsync();
        }
    }
}
