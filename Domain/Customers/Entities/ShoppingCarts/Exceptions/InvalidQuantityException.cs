﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entities.ShoppingCarts.Exceptions
{
    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException() : base(message: "Invalid quantity.")
        {
        }
    }
}
