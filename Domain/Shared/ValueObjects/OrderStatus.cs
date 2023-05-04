using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.ValueObjects
{
    public enum OrderStatus
    {
        InProgress,
        WaitingForPayment = 10,
        PaymentReceived = 20,
        PackingInProgress = 30,
        OrderPacked = 40,
        WaitingForShipping = 50,
        OrderShipped = 60,
        Cancelled = 70,
        Closed = 80,
        Completed = 90
    }
}
