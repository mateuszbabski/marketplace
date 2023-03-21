using Domain.Shared.Abstractions;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Rules
{
    public class SameCurrencyMoneyOperationRule : IBusinessRule
    {
        private readonly MoneyValue _left;
        private readonly MoneyValue _right;

        public SameCurrencyMoneyOperationRule(MoneyValue left, MoneyValue right)
        {
            _left = left;
            _right = right;
        }
        public string Message => "Money currencies must be the same";

        public bool IsBroken() => _left.Currency != _right.Currency;
    }
}
