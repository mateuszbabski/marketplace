namespace Domain.Entrepreneur.Exceptions
{
    public class EmptyEntrepreneurIdException : Exception
    {
        public EmptyEntrepreneurIdException() : base(message: "Entrepreneur Id cannot be empty.")
        {

        }
    }
}
