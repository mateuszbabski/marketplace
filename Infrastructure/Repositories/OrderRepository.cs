using Domain.Customers.Entities.Orders;
using Domain.Customers.Entities.Orders.Repositories;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
