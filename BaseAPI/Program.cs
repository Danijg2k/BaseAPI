using BaseAPI.Configuration;
using BaseAPI.Context;
using BaseAPI.Middleware.Extensions;
using BaseAPI.Middleware.Settings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Logger (Microsoft Extensions Logging NuGet)
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add Custom Logging Service to services collection
// builder.Services.AddTransient<ILoggingService, LoggingService>();

// Add services to the container.
builder.Services.AddControllers();

// Add created context
builder.Services.AddDbContext<CityContext>(opt =>
    opt.UseInMemoryDatabase("CityList"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();     // Only required for minimal APIs

// Configure Swagger
builder.Services.ConfigureSwagger();

var app = builder.Build();

// Get MiddlewareSettings section from appsettings.json
var middlewareSettings = builder.Configuration.GetSection("MiddlewareSettings").Get<MiddlewareSettings>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add custom Middleware

/* 
 * Option 1 - Add directly
   app.UseMiddleware<LoggingMiddleware>();
   app.UseMiddleware<TimeLoggingMiddleware>();
   app.UseMiddleware<ErrorHandlerMiddleware>();
*/

// Option 2 - Add via extension method (from Middleware Extensions)
app.UseLoggingMiddleware();
if (middlewareSettings.UseTimeLoggingMiddleware)
{
    app.UseTimeLoggingMiddleware();     // Will measure error handling time (added in NEXT line), but not logging time (LAST line)
}
app.UseErrorHandlerMiddleware();
//

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
