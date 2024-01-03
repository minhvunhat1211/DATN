namespace VnEdu.Api.Contracts.User.Request
{
    /// <summary>
    /// Information of UserUpdate
    /// CreatedBy: MinhVN(22/12/2022)
    /// </summary>
    public class UserUpdate
    {
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// DecentralizationId
        /// </summary>
        public int DecentralizationId { get; set; }
    }
}
