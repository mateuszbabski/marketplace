namespace Domain.Shared.Exceptions
{
    public class InvalidTelephoneNumberException : Exception
    {
        public InvalidTelephoneNumberException() : base(message: "Invalid number.")
        {
        }
    }
}
