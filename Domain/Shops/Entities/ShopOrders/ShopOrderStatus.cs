using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shops.Entities.ShopOrders
{
    public enum ShopOrderStatus
    {
        InProgress,
        WaitingForPayment,
        PaymentReceived,
        PackingInProgress,
        OrderPacked,
        WaitingForShipping,
        OrderShipped,
        Cancelled,
        Closed,
        Completed
    }
}
