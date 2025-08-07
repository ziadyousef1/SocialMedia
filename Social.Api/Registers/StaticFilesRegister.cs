using Social.Api.Registers;

namespace Social.Api.Registers
{
    public class StaticFilesRegister : IWebApplicationRegister
    {
        public void RegisterPipelineComponents(WebApplication app)
        {
            // Create uploads directory if it doesn't exist
            var uploadsPath = Path.Combine(app.Environment.WebRootPath ?? "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsPath);
            
            // Enable serving static files
            app.UseStaticFiles();
        }
    }
}
