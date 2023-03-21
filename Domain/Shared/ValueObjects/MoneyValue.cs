using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.ValueObjects
{
    public record MoneyValue
    {
        public decimal Value { get;}
        public Currency Currency { get; }

        public MoneyValue(decimal value, Currency currency)
        {
            Value = value;
            Currency = currency;    
        }
    }
}
