using BaseAPI.Context;
using BaseAPI.Logging;
using BaseAPI.Middleware;
using BaseAPI.Middleware.Extensions;
using BaseAPI.Middleware.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add Logging Service to services collection
builder.Services.AddTransient<ILoggingService, LoggingService>();

// Add services to the container.
builder.Services.AddControllers();

// Add created context
builder.Services.AddDbContext<CityContext>(opt =>
    opt.UseInMemoryDatabase("CityList"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();     // Only required for minimal APIs
builder.Services.AddSwaggerGen(options =>       // Configure Swagger
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "BaseAPI",
        Description = "An ASP.NET Core 6.0 Web API that can be used as a template for future projects",
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://www.linkedin.com/in/daniel-jimenez-gutierrez/")
        }
    });

    // Configure Swagger to use XML (Property 'GenerateDocumentationFile' must be included in {NameOfProject.csproj}
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

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
