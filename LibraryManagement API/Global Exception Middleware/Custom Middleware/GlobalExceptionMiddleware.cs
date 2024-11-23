using LibraryManagement_API.Custom_Error_Responses;

namespace LibraryManagement_API.Global_Exception_Middleware.Custom_Middleware
{
    public class GlobalExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Proceed to the next middleware
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorResponse = new ErrorResponse
            {
                Message = "An unexpected error occurred.",
                Detail = exception.Message, // Avoid exposing in production
                TraceId = context.TraceIdentifier
            };

            return context.Response.WriteAsJsonAsync(errorResponse);
        }

    }
}
