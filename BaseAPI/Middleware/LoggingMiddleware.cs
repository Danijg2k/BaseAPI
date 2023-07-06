using BaseAPI.Logging;
using BaseAPI.Utilities.Constants;

namespace BaseAPI.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log the incoming request path
            _logger.Log(LogLevel.Information, LoggingConstants.requestPath, context.Request.Path);

            await _next(context);

            // Get distinct response headers
            var uniqueResponseHeaders = context.Response.Headers
                                                        .Select(x => x.Key)
                                                        .Distinct();
            // And log them
            _logger.LogInformation(LoggingConstants.headers, string.Join(", ", uniqueResponseHeaders));
        }
    }
}
