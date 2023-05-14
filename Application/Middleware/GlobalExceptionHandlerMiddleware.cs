using Application.Common.Exceptions;
using Application.Common.Responses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using System.Net;

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
                LogContext.PushProperty("ErrorId", errorId);
                LogContext.PushProperty("StackTrace", exception.StackTrace);

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
                    case CustomValidationException validationException:
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

                Log.Error("{@Exception} Request failed with Status Code {@StatusCode} and Error Id {@ErrorId}.", 
                    errorResult.Exception,
                    errorResult.StatusCode, 
                    errorId);

                var response = context.Response;
                if (!response.HasStarted)
                {
                    response.ContentType = "application/json";
                    response.StatusCode = errorResult.StatusCode;
                    await response.WriteAsync(JsonConvert.SerializeObject(errorResult));
                }
                else
                {
                    Log.Warning("Can't write error response. Response has already started.");
                }
                    
            }
        }
    }
}
