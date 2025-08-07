using Social.Api.Registers;

namespace Social.Api.Extentions
{
    public static class RegisterExtentions
    {
        public static void RegisterServices(this WebApplicationBuilder builder ,Type scanningType)
        {
            var registerTypes = scanningType.Assembly.GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IWebApplicationBuilderRegister)));
            foreach (var registerType in registerTypes)
            {
                var register = Activator.CreateInstance(registerType) as IWebApplicationBuilderRegister;
                register?.RegisterServices(builder);
            }

        }
        public static void RegisterPipelineComponents(this WebApplication app,Type scanningType)
        {
            var registerTypes = scanningType.Assembly.GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IWebApplicationRegister)));
            foreach (var registerType in registerTypes)
            {
                var register = Activator.CreateInstance(registerType) as IWebApplicationRegister;
                register?.RegisterPipelineComponents(app);
            }

        }
    }
}