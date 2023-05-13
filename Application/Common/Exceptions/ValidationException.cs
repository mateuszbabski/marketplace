using FluentValidation.Results;
using System.Net;

namespace Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; }
        public HttpStatusCode StatusCode { get; }

        public ValidationException(HttpStatusCode statusCode = HttpStatusCode.UnprocessableEntity) : base("Validation failure")
        {
            Errors = new List<string>();
            StatusCode = statusCode;
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }        
    }
}
