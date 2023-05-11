using Application.Common.Exceptions;
using Application.Common.Responses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Middleware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try 
            {
                await next.Invoke(context);
            }
            catch (Exception exception) 
            { 
                string errorId = Guid.NewGuid().ToString();

                var errorResult = new ErrorResponse
                {
                    Source = exception.TargetSite?.DeclaringType?.FullName,
                    Exception = exception.Message.Trim(),
                    ErrorId = errorId
                };

                errorResult.Messages.Add(exception.Message);

                if (exception is not CustomException && exception.InnerException != null)
                {
                    while (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                    }
                }

                switch (exception)
                {
                    case ValidationException validationException:
                        errorResult.StatusCode = (int)validationException.StatusCode;
                        if (validationException.Errors is not null)
                        {
                            errorResult.Messages = validationException.Errors;
                        }
                        break;

                    case CustomException customException:
                        errorResult.StatusCode = (int)customException.StatusCode;
                        if (customException.ErrorMessages is not null)
                        {
                            errorResult.Messages = customException.ErrorMessages;
                        }
                        break;

                    default:
                        errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                // LOG

                var response = context.Response;
                if (!response.HasStarted)
                {
                    response.ContentType = "application/json";
                    response.StatusCode = errorResult.StatusCode;
                    await response.WriteAsync(JsonConvert.SerializeObject(errorResult));
                }
                else
                {
                    // LOG
                    return;
                }
                    
            }
        }
    }
}
