using System.Net;

namespace Application.Common.Exceptions
{
    public class ForbidException : CustomException
    {
        public ForbidException(string message) 
            : base(message, null, HttpStatusCode.Forbidden)
        {
        }
    }
}
