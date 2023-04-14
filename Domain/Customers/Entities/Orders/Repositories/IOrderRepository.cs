using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entities.Orders.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> Add(Order order);
        Task Update(Order order);
    }
}
