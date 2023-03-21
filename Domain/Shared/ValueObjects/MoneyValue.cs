using Domain.Shared.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.ValueObjects
{
    public record MoneyValue
    {
        public decimal Value { get; }
        public Currency Currency { get; } = Currency.USD;

        private MoneyValue(decimal value, Currency currency)
        {
            Value = value;
            Currency = currency;
        }

        public static MoneyValue Of(decimal value, Currency currency)
        {
            if (new MoneyMustHaveCurrencyRule(currency.ToString()).IsBroken())
            {
                throw new Exception("Invalid currency");
            }

            if (value < 0)
            {
                throw new Exception("Money amount value cannot be negative");
            }

            return new MoneyValue(value, currency);
        }

        public static MoneyValue Of(MoneyValue value)
        {
            return new MoneyValue(value.Value, value.Currency);
        }

        public static MoneyValue operator +(MoneyValue left, MoneyValue right)
        {
            if (new SameCurrencyMoneyOperationRule(left, right).IsBroken())
            {
                throw new Exception("Currency must be equal");
            }

            return new MoneyValue(left.Value + right.Value, left.Currency);
        }

        public static MoneyValue operator *(int number, MoneyValue right)
        {
            return new MoneyValue(number * right.Value, right.Currency);
        }

        public static MoneyValue operator *(decimal number, MoneyValue right)
        {
            return new MoneyValue(number * right.Value, right.Currency);

        }
    }
}
