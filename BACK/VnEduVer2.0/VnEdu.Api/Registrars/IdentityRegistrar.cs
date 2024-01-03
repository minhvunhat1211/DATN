using VnEdu.Core.Entities.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace VnEdu.Api.Registrars
{
    /// <summary>
    /// Information off IdentityRegistrar
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public class IdentityRegistrar : IWebApplicationBuilderRegistrar
    {
        /// <summary>
        /// RegisterServices
        /// </summary>
        /// <param name="builder">Builder</param>
        /// CreatedBy: MinhVN(20/12/2022)
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var jwtSettings = new JwtSettings();
            builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);

            var jwtSection = builder.Configuration.GetSection(nameof(JwtSettings));
            builder.Services.Configure<JwtSettings>(jwtSection);

            builder.Services
                .AddAuthentication(a =>
                {
                    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwt =>
                {
                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SigningKey)),
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidateAudience = true,
                        ValidAudiences = jwtSettings.Audiences,
                        RequireExpirationTime = false,
                        ValidateLifetime = true,
                    };
                    jwt.Audience = jwtSettings.Audiences[0];
                    jwt.ClaimsIssuer = jwtSettings.Issuer;
                });
        }
    }
}
