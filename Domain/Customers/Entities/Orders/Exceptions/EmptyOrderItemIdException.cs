using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entities.Orders.Exceptions
{
    public class EmptyOrderItemIdException : Exception
    {
        public EmptyOrderItemIdException() : base(message: "OrderItemId can not be empty.")
        {
        }
    }
}
