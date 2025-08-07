using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Social.Api.Options
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var discription in _provider.ApiVersionDescriptions)
            {

                options.SwaggerDoc(discription.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Social API",
                    Version = discription.ApiVersion.ToString()
                });
            }
            options.AddSecurityDefinition("Bearer", CreateSecurityScheme());
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    CreateSecurityScheme(),
                    Array.Empty<string>()
                }
            });
        }

        private OpenApiSecurityScheme CreateSecurityScheme()
        {
            return new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Scheme = "bearer",
                BearerFormat = "JWT",
                Type = SecuritySchemeType.Http,
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
        }
    }
}
