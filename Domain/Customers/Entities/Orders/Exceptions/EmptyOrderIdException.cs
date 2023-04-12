using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entities.Orders.Exceptions
{
    public class EmptyOrderIdException : Exception
    {
        public EmptyOrderIdException() : base(message: "OrderId can not be empty.")
        {
        }
    }
}
