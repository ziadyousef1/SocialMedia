
using Microsoft.EntityFrameworkCore;
using Social.DAL;

namespace Social.Api.Registers
{
    public class DbRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DataContext>(options =>options.UseSqlServer(ConnectionString));

        }
    }
}
