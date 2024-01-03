namespace VnEdu.Api.Contracts.SchoolYear.Request
{
    /// <summary>
    /// InFormation of SchoolYearCreate
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    public class SchoolYearCreate
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
