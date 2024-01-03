using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VnEdu.Core.Entities.Options;

namespace VnEdu.Core.Services
{
    /// <summary>
    /// Information of IdentityService
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    public class IdentityService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly byte[] _key;

        public IdentityService(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
            _key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);
        }

        /// <summary>
        /// TokenHandler
        /// CreatedBy: MinhVN(23/12/2022)
        /// </summary>
        public JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();

        /// <summary>
        /// CreateSecurityToken
        /// </summary>
        /// <param name="identity">ClaimsIdentity</param>
        /// <returns>SecurityToken</returns>
        /// CreatedBy: MinhVN(23/12/2022)s
        public SecurityToken CreateSecurityToken(ClaimsIdentity identity)
        {
            var tokenDescriptor = GetTokenDescriptor(identity);

            return TokenHandler.CreateToken(tokenDescriptor);
        }

        /// <summary>
        /// WriteToken
        /// </summary>
        /// <param name="token">SecurityToken</param>
        /// <returns>Token</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public string WriteToken(SecurityToken token)
        {
            return TokenHandler.WriteToken(token);
        }

        /// <summary>
        /// GetTokenDescriptor
        /// </summary>
        /// <param name="identity">ClaimsIdentity</param>
        /// <returns>SecurityTokenDescriptor</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        private SecurityTokenDescriptor GetTokenDescriptor(ClaimsIdentity identity)
        {
            return new SecurityTokenDescriptor()
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddHours(23),
                Audience = _jwtSettings.Audiences[0],
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
        }
    }
}