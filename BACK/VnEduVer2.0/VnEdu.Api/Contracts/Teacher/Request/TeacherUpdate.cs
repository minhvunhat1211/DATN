namespace VnEdu.Api.Contracts.Teacher.Request
{
    /// <summary>
    /// Information of TeacherUpdate
    /// CreatedBy: MinhVN(24/12/2022)
    /// </summary>
    public class TeacherUpdate
    {
        /// <summary>
        /// TeacherCode
        /// </summary>
        public string TeacherCode { get; set; } = string.Empty;

        /// <summary>
        /// FullName
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Gender
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// DateOfBirth
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// NumberCard
        /// </summary>
        public string NumberCard { get; set; } = string.Empty;

        /// <summary>
        /// AddressCurent
        /// </summary>
        public string AddressCurent { get; set; } = string.Empty;

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; } = string.Empty;
    }
}
