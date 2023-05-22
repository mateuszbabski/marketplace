using Domain.Shared.Rules;
using Domain.Shops.Entities.Products.Exceptions;

namespace Domain.Shared.ValueObjects
{
    public record MoneyValue
    {
        public decimal Amount { get; }
        public string Currency { get; }   

        internal MoneyValue(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static MoneyValue Of(decimal amount, string currency)
        {
            if (new SystemMustAcceptsCurrencyRule(currency).IsBroken() || currency.Length != 3)
            {
                throw new InvalidProductPriceException("Invalid currency.");
            }

            if (amount <= 0)
            {
                throw new InvalidProductPriceException("Money amount value cannot be zero or negative.");
            }

            return new MoneyValue(amount, currency);
        }

        public static MoneyValue Of(MoneyValue value)
        {
            return new MoneyValue(value.Amount, value.Currency);
        }

        public static MoneyValue operator +(MoneyValue left, MoneyValue right)
        {
            if (new SameCurrencyMoneyOperationRule(left, right).IsBroken())
            {
                throw new Exception("Currency must be equal");
            }

            return new MoneyValue(left.Amount + right.Amount, left.Currency);
        }

        public static MoneyValue operator *(int number, MoneyValue right)
        {
            return new MoneyValue(number * right.Amount, right.Currency);
        }

        public static MoneyValue operator *(decimal number, MoneyValue right)
        {
            return new MoneyValue(number * right.Amount, right.Currency);
        }
    }
}
