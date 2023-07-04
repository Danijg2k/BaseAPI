using BaseAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
