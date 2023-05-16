using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.ValueObjects;
using Domain.Shops.Entities.ShopOrders.ValueObjects;
using Domain.Shops.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shops.Entities.ShopOrders.Repositories
{
    public interface IShopOrderRepository
    {
        Task<ShopOrder> Add(ShopOrder order);
        Task<IEnumerable<ShopOrder>> GetAllShopOrdersForShop(ShopId shopId);
        Task<ShopOrder> GetShopOrderById(ShopOrderId Id, ShopId shopId);
        Task<IEnumerable<ShopOrder>> GetShopOrdersByOrderId(OrderId orderId);
        Task<IEnumerable<ShopOrder>> GetAllShopOrdersByOrderIdForCustomer(OrderId orderId, CustomerId customerId);
    }
}
