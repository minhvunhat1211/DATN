using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VnEdu.Core.Entities.Models
{
    /// <summary>
    /// Information of Student_Subject
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public class Student_Subject : BaseModel
    {
        /// <summary>
        /// DetailedTableScoreId
        /// </summary>
        [Key]
        public int DetailedTableScoreId { get; set; }

        /// <summary>
        /// StudentId
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// SubjectId
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// SemesterId
        /// </summary>
        public int SemesterId { get; set; }

        /// <summary>
        /// SchoolYearId
        /// </summary>
        public int SchoolYearId { get; set; }
    }
}