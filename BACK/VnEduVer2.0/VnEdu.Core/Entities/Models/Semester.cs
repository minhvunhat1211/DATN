using System.ComponentModel.DataAnnotations;

namespace VnEdu.Core.Entities.Models
{
    /// <summary>
    /// Information of Semester
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public class Semester : BaseModel
    {
        /// <summary>
        /// SemesterId
        /// </summary>
        [Key]
        public int SemesterId { get; set; }

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