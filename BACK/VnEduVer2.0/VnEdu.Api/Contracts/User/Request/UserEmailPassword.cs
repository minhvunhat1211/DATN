namespace VnEdu.Api.Contracts.User.Request
{
    /// <summary>
    /// Information of UserEmailPassword
    /// CreatedBy: MinhVN(22/12/2022)
    /// </summary>
    public class UserEmailPassword
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
