using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shops.Entities.ShopOrders.Exceptions
{
    public class EmptyShopOrderItemIdException : Exception
    {
        public EmptyShopOrderItemIdException() : base(message: "ShopOrderItemId can not be empty.")
        {
        }
    }
}
