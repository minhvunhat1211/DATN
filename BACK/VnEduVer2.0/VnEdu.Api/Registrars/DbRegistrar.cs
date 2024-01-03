using Microsoft.EntityFrameworkCore;
using VnEdu.Infrastructure.Data;

namespace VnEdu.Api.Registrars
{
    /// <summary>
    /// Information of DbRegistrar
    /// CreatedBy: MinhVN(24/12/2022)
    /// </summary>
    public class DbRegistrar : IWebApplicationBuilderRegistrar
    {
        /// <summary>
        /// RegisterServices
        /// </summary>
        /// <param name="builder">WebApplicationBuilder</param>
        /// CreatedBy: MinhVN(24/12/2022)
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString,
                options => options.EnableRetryOnFailure()));
        }
    }
}