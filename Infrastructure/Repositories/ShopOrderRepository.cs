using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.Entities.Orders;
using Domain.Customers.ValueObjects;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Shops.Entities.ShopOrders;
using Domain.Shops.Entities.ShopOrders.ValueObjects;
using Domain.Shops.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Domain.Shops.Entities.ShopOrders.Repositories;

namespace Infrastructure.Repositories
{
    internal sealed class ShopOrderRepository : IShopOrderRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public ShopOrderRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ShopOrder> Add(ShopOrder order)
        {
            await _dbContext.ShopOrders.AddAsync(order);
            return order;
        }

        public async Task<ShopOrder> GetShopOrderById(ShopOrderId Id, ShopId shopId)
        {
            return await _dbContext.ShopOrders.Where(o => o.ShopId == shopId)
                                              .Include(o => o.ShopOrderItems)
                                              .FirstOrDefaultAsync(o => o.Id == Id);
        }

        public async Task<IEnumerable<ShopOrder>> GetAllShopOrdersForCustomer(ShopId shopId)
        {
            return await _dbContext.ShopOrders.Where(o => o.ShopId == shopId)
                                              .ToListAsync();
        }
    }
}
