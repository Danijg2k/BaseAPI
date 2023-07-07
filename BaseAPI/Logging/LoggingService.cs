namespace BaseAPI.Logging
{
    public class LoggingService : ILoggingService   // Not used currently. Using Logging from Microsoft.Extensions.Logging (check Middlewares)
    {
        public void Log(LogLevel level, string content, string message)
        {
            // Visible from debug output console
            // System.Diagnostics.Trace.WriteLine($"[{level,-11}] - {content,-16} - {message}");
            // Visible from console
            Console.WriteLine($"[{level, -11}] - {content, -16} - {message}");                   
        }
    }

    public interface ILoggingService
    {
        public void Log(LogLevel level, string content, string message);
    }
}
