namespace VnEdu.Core.Entities.Models
{
    /// <summary>
    /// Information of Subject
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public class Subject : BaseModel
    {
        /// <summary>
        /// SubjectId
        /// </summary>
        public int SubjectId { get; set; }

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