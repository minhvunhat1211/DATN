using Microsoft.OpenApi.Models;
using VnEdu.Api.Filters;
using VnEdu.Api.Options;

namespace VnEdu.Api.Registrars
{
    /// <summary>
    /// Information of WebApplicationBuilderRegistrar
    /// CreatedBy: MinhVN(24/12/2022)
    /// </summary>
    public class WebApplicationBuilderRegistrar : IWebApplicationBuilderRegistrar
    {
        /// <summary>
        /// RegisterServices
        /// </summary>
        /// <param name="builder">WebApplicationBuilder</param>
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "VnEdu Api", Version = "v1" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "VnEdu.Api.xml");
                config.IncludeXmlComments(filePath);
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            builder.Services.ConfigureOptions<ConfiguraSwagerOption>();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddMvc(options => options.Filters.Add(new VnEduExceptionAttribute()));
        }
    }
}