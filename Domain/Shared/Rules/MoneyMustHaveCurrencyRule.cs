using Domain.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Rules
{
    public class MoneyMustHaveCurrencyRule : IBusinessRule
    {
        private readonly string _currency;
        public MoneyMustHaveCurrencyRule(string currency)
        {

            _currency = currency;

        }
        public string Message => "Money value must have currency";

        public bool IsBroken() => string.IsNullOrWhiteSpace(_currency);        
    }
}
