namespace Domain.Entrepreneur.Exceptions
{
    public class EmptyTaxNumberException : Exception
    {
        public EmptyTaxNumberException() : base(message: "Tax number cannot be empty.")
        {
            
        }
    }
}
