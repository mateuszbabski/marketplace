namespace Domain.Entrepreneur.Exceptions
{
    public class EmptyShopNameException : Exception
    {
        public EmptyShopNameException() : base(message: "Shop name cannot be empty.")
        {
        }
    }
}
