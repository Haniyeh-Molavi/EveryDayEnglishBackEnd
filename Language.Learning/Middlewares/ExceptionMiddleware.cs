namespace LanguageLearning.API.Middlewares;

// Catches errors from the controllers and services and returns a clear JSON answer:
//   ArgumentException           -> 400 Bad Request
//   UnauthorizedAccessException -> 401 Unauthorized
//   KeyNotFoundException        -> 404 Not Found
//   anything else               -> 500 Internal Server Error
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var statusCode = ex switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var message = ex.Message;

            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                // Log the real error, but don't show internal details to the client
                _logger.LogError(ex, "Unhandled error");
                message = "Something went wrong. Please try again later.";
            }

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(new { error = message });
        }
    }
}
