namespace Domain.Shared.Exceptions
{
    public class EmptyNameException : Exception
    {
        public EmptyNameException() : base(message: "Name or LastName field cannot be empty.")
        {
        }
    }
}
