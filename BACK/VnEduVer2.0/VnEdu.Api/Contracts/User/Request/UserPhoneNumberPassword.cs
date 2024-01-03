namespace VnEdu.Api.Contracts.User.Request
{
    /// <summary>
    /// Information of UserPhoneNumberPassword
    /// CreatedBy: MinhVN(22/12/2022)
    /// </summary>
    public class UserPhoneNumberPassword
    {
        /// <summary>
        /// PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
