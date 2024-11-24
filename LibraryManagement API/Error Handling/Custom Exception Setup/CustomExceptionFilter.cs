using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace LibraryManagement_API.Error_Handling.Custom_Exception_Setup
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Unhandled exception occurred.");

            // Check for custom exceptions
            if (context.Exception is CustomApiException customException)
            {
                context.Result = new JsonResult(new
                {
                    Message = customException.Message,
                    StatusCode = customException.StatusCode,
                    TraceId = context.HttpContext.TraceIdentifier
                })
                {
                    StatusCode = customException.StatusCode
                };
            }
            else
            {
                // Handle generic exceptions
                context.Result = new JsonResult(new
                {
                    Message = "An unexpected error occurred.",
                    TraceId = context.HttpContext.TraceIdentifier
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            context.ExceptionHandled = true;
        }
    }
}
