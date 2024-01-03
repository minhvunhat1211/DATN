using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VnEdu.Api.Options
{
    /// <summary>
    /// Information of ConfiguraSwagerOption
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public class ConfiguraSwagerOption : IConfigureOptions<SwaggerGenOptions>
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="options">Options</param>
        /// CreatedBy: MinhVN(20/12/2022)
        public void Configure(SwaggerGenOptions options)
        {
            var schema = GetJwtSecuritySchema();
            options.AddSecurityDefinition(schema.Reference.Id, schema);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {schema, Array.Empty<string>()}
            });
        }

        /// <summary>
        /// GetJwtSecuritySchema
        /// </summary>
        /// <returns>OpenApiSecurityScheme</returns>
        /// CreatedBy: MinhVN(20/12/2022)
        private static OpenApiSecurityScheme GetJwtSecuritySchema()
        {
            return new OpenApiSecurityScheme
            {
                Name = "Jwt Authentication",
                Description = "Provide a JWT Bearer",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
        }
    }
}