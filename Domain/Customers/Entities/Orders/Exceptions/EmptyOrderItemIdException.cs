namespace Domain.Customers.Entities.Orders.Exceptions
{
    public class EmptyOrderItemIdException : Exception
    {
        public EmptyOrderItemIdException() : base(message: "OrderItemId can not be empty.")
        {
        }
    }
}
