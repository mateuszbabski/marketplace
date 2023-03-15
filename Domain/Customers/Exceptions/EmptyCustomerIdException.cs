using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Exceptions
{
    public class EmptyCustomerIdException : Exception
    {
        public EmptyCustomerIdException() : base(message: "Customer Id cannot be empty")
        {

        }
    }
}
