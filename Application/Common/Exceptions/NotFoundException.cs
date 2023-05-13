﻿using System.Net;

namespace Application.Common.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) 
            : base(message, null, HttpStatusCode.NotFound)
        {
        }
    }
}
