namespace Domain.Shops.Exceptions
{
    public class EmptyTaxNumberException : Exception
    {
        public EmptyTaxNumberException() : base(message: "Tax number cannot be empty.")
        {
            
        }
    }
}
