namespace Domain.Shared.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base(message: "Invalid email.")
        {
        }
    }
}
