using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BaseAPI.Configuration
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>       
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
        }
    }
}
