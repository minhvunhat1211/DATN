namespace VnEdu.Api.Contracts.Subject.Request
{
    /// <summary>
    /// Information of SubjectUpdate
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    public class SubjectUpdate
    {
        /// <summary>
        /// SubjectName
        /// </summary>
        public string SubjectName { get; set; } = string.Empty;

        /// <summary>
        /// PeriodsPerYear
        /// </summary>
        public int PeriodsPerYear { get; set; }

        /// <summary>
        /// Credit
        /// </summary>
        public int Credit { get; set; }
    }
}
