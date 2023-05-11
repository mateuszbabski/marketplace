using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
