using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entities.ShoppingCarts.Exceptions
{
    public class EmptyShoppingCartIdException : Exception
    {
        public EmptyShoppingCartIdException() : base(message: "Shopping Cart Id can not be empty.")
        {
            
        }
    }
}
