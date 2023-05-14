using FluentValidation.Results;
using System.Net;

namespace Application.Common.Exceptions
{
    public class CustomValidationException : Exception
    {
        public List<string> Errors { get; }
        public HttpStatusCode StatusCode { get; }

        public CustomValidationException(HttpStatusCode statusCode = HttpStatusCode.UnprocessableEntity) : base("Validation failure")
        {
            Errors = new List<string>();
            StatusCode = statusCode;
        }

        public CustomValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}
