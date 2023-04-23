using Domain.Customers.Entities.Orders.Exceptions;
using Domain.Shops.Entities.ShopOrders.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shops.Entities.ShopOrders.ValueObjects
{
    public record ShopOrderItemId
    {
        public Guid Value { get; }

        public ShopOrderItemId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyShopOrderItemIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(ShopOrderItemId id) => id.Value;
        public static implicit operator ShopOrderItemId(Guid value) => new(value);
    }
}
