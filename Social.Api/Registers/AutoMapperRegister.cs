
using Social.Application.MappingProfiles;
using Social.Application.UserProfiles.Queries;

namespace Social.Api.Registers
{
    public class AutoMapperRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
           builder.Services.AddAutoMapper(typeof(UserProfileMap));
           builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GellAllUserProfilesQuery).Assembly));
        }
    }
}
