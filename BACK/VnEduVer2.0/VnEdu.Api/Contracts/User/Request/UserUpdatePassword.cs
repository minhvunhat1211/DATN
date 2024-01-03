namespace VnEdu.Api.Contracts.User.Request
{
    /// <summary>
    /// Information of UserUpdatePassword
    /// CreatedBy: MinhVN(22/12/2022)
    /// </summary>
    public class UserUpdatePassword
    {
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
