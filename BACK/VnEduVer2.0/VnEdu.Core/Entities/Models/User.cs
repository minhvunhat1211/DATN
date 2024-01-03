using System.ComponentModel.DataAnnotations;

namespace VnEdu.Core.Entities.Models
{
    /// <summary>
    /// Information of User
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public class User
    {
        /// <summary>
        /// UserId
        /// </summary>
        [Key]
        public int UserId { get; set; }

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
        /// Password
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// DecentralizationId
        /// </summary>
        public int DecentralizationId { get; set; }

        /// <summary>
        /// CodeOTP
        /// </summary>
        public string CodeOTP { get; set; } = string.Empty;

        /// <summary>
        /// TimeCreateOTP
        /// </summary>
        public DateTime TimeCreateOTP { get; set; }
    }
}