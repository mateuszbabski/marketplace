using System.Net;

namespace Application.Common.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message) 
            : base(message, null, HttpStatusCode.BadRequest)
        {
        }
    }
}
