using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Social.Api.Options
{
    public class ConfigureSwaggerrOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerrOptions(IApiVersionDescriptionProvider provider)
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
        }
    }
}
