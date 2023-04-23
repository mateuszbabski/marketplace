using Domain.Customers.Entities.Orders.Exceptions;
using Domain.Shops.Entities.ShopOrders.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shops.Entities.ShopOrders.ValueObjects
{
    public record ShopOrderId
    {
        public Guid Value { get; }

        public ShopOrderId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyShopOrderIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(ShopOrderId id) => id.Value;
        public static implicit operator ShopOrderId(Guid value) => new(value);
    }
}
