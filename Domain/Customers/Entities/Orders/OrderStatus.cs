﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entities.Orders
{
    public enum OrderStatus
    {
        InProgress,
        WaitingForPayment,
        PaymentReceived,        
        OrderPacked,
        OrderShipped,
        Cancelled,
        Closed,
        Completed
    }
}
