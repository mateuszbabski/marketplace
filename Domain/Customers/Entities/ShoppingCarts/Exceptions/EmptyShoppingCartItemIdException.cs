using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entities.ShoppingCarts.Exceptions
{
    public class EmptyShoppingCartItemIdException : Exception
    {
        public EmptyShoppingCartItemIdException() : base(message: "Shopping Cart Item Id can not be empty.")
        {
            
        }
    }
}
