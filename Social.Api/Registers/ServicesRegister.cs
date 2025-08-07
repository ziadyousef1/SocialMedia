using Social.Api.Registers;
using Social.Api.Services;
using Social.Application.Services;

namespace Social.Api.Registers
{
    public class ServicesRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IFileUploadService, LocalFileUploadService>();
        }
    }
}
