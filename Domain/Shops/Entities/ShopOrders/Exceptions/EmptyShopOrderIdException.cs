using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shops.Entities.ShopOrders.Exceptions
{
    public class EmptyShopOrderIdException : Exception
    {
        public EmptyShopOrderIdException() : base(message: "ShopOrderId can not be empty.")
        {
        }
    }
}
