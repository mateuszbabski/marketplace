namespace Domain.Shared.ValueObjects
{
    public record Currency(string Symbol)
    {
        public static Currency PLN => new("PLN");
        public static Currency EUR => new("EUR");
        public static Currency USD => new("USD");
    }
}
