
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Social.Api.Registers
{
    public class MvcWebAppRegister : IWebApplicationRegister
    {
        public void RegisterPipelineComponents(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var discription in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{discription.GroupName}/swagger.json",
                        discription.GroupName.ToUpperInvariant());
                }

            });
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
