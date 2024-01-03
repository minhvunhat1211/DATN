namespace VnEdu.Api.Contracts.SchoolYear.Request
{
    /// <summary>
    /// InFormation of SchoolYearUpdate
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    public class SchoolYearUpdate
    {
        /// <summary>
        /// SchoolYearName
        /// </summary>
        public string SchoolYearName { get; set; } = string.Empty;

        /// <summary>
        /// DateStart
        /// </summary>
        public DateTime? DateStart { get; set; }

        /// <summary>
        /// DateEnd
        /// </summary>
        public DateTime? DateEnd { get; set; }
    }
}
