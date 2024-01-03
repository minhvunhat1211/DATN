namespace VnEdu.Core.Entities.Options
{
    /// <summary>
    /// Information of JwtSettings
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// SigningKey
        /// </summary>
        public string SigningKey { get; set; } = string.Empty;

        /// <summary>
        /// Issuer
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Audiences
        /// </summary>
        public string[] Audiences { get; set; } = default!;
    }
}