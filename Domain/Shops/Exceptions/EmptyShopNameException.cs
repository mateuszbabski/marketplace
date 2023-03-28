namespace Domain.Shops.Exceptions
{
    public class EmptyShopNameException : Exception
    {
        public EmptyShopNameException() : base(message: "Shop name cannot be empty.")
        {
        }
    }
}
