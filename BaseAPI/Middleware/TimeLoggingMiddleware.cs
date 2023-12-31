﻿using BaseAPI.Utilities.Constants;
using System.Diagnostics;

namespace BaseAPI.Middleware
{
    public class TimeLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TimeLoggingMiddleware> _logger;

        public TimeLoggingMiddleware(RequestDelegate next, ILogger<TimeLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            await _next(context);

            watch.Stop();
            _logger.LogInformation(LoggingConstants.execTime, watch.ElapsedMilliseconds);
        }
    }
}
