namespace VnEdu.Api.Contracts.Class.Request
{
    /// <summary>
    /// Information of ClassCreate
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public class ClassCreate
    {
        /// <summary>
        /// ClassName
        /// </summary>
        public string ClassName { get; set; } = string.Empty;

        /// <summary>
        /// TeacherId
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// Grade
        /// </summary>
        public string Grade { get; set; } = string.Empty;

        /// <summary>
        /// Room
        /// </summary>
        public string Room { get; set; } = string.Empty;

        /// <summary>
        /// SchoolYearId
        /// </summary>
        public int SchoolYearId { get; set; }
    }
}
