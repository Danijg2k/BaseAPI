using BaseAPI.Middleware;

namespace BaseAPI.Middleware.Extensions
{
    public static class MiddlewareExtensions
    {
        // Error Handler Middleware (Handle http responses)
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlerMiddleware>();
        }

        // Logging Middleware (Request path, response headers)
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggingMiddleware>();
        }

        // Time Logging (Request execution time)
        public static IApplicationBuilder UseTimeLoggingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TimeLoggingMiddleware>();
        }
    }
}
