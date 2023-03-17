namespace Domain.Customers.Exceptions
{
    public class EmptyCustomerIdException : Exception
    {
        public EmptyCustomerIdException() : base(message: "Customer Id cannot be empty")
        {

        }
    }
}
