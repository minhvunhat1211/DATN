namespace VnEdu.Api.Contracts.Semester.Request
{
    /// <summary>
    /// Information of SemesterCreate
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    public class SemesterCreate
    {
        /// <summary>
        /// SemesterName
        /// </summary>
        public string SemesterName { get; set; } = string.Empty;

        /// <summary>
        /// DateStart
        /// </summary>
        public DateTime? DateStart { get; set; }

        /// <summary>
        /// DateEnd
        /// </summary>
        public DateTime? DateEnd { get; set; }

        /// <summary>
        /// SchoolYearId
        /// </summary>
        public int SchoolYearId { get; set; }
    }
}
