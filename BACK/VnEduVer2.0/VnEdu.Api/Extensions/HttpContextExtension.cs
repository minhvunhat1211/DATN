using System.Security.Claims;

namespace VnEdu.Api.Extensions
{
    /// <summary>
    /// Information of HttpContextExtension
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    public static class HttpContextExtension
    {
        /// <summary>
        /// GetUserNameClaimValue
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>string</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public static string GetUserNameClaimValue(this HttpContext context)
        {
            return GetNameClaimValue("UserName", context);
        }

        /// <summary>
        /// GetUserIdClaimValue
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>string</returns>
        /// CreatedBy: MinhVN(06/01/2022)
        public static string GetUserIdClaimValue(this HttpContext context)
        {
            return GetNameClaimValue("UserId", context);
        }

        /// <summary>
        /// GetNameClaimValue
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="context">HttpContext</param>
        /// <returns>string</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        private static string GetNameClaimValue(string key, HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;

            return identity?.FindFirst($"{key}")?.Value;
        }
    }
}
