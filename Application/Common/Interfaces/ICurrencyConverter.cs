﻿using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICurrencyConverter
    {
        Task<decimal> GetConversionRate(string from, string to);
        Task<decimal> GetConvertedPrice(decimal amount, string from, string to);
    }
}
