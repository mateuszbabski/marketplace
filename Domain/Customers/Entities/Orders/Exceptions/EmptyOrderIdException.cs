namespace Domain.Customers.Entities.Orders.Exceptions
{
    public class EmptyOrderIdException : Exception
    {
        public EmptyOrderIdException() : base(message: "OrderId can not be empty.")
        {
        }
    }
}
