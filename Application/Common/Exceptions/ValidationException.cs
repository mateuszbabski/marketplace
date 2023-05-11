using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; }
        public HttpStatusCode StatusCode { get; }

        public ValidationException(HttpStatusCode statusCode = HttpStatusCode.Forbidden) : base("Validation failure")
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
