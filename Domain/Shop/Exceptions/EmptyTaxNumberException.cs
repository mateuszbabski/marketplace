namespace Domain.Shop.Exceptions
{
    public class EmptyTaxNumberException : Exception
    {
        public EmptyTaxNumberException() : base(message: "Tax number cannot be empty.")
        {
            
        }
    }
}
