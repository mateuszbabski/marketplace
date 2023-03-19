namespace Domain.Shop.Exceptions
{
    public class EmptyShopNameException : Exception
    {
        public EmptyShopNameException() : base(message: "Shop name cannot be empty.")
        {
        }
    }
}
