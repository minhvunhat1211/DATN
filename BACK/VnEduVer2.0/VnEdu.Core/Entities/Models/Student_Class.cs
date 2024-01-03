using System.ComponentModel.DataAnnotations;

namespace VnEdu.Core.Entities.Models
{
    /// <summary>
    /// Information of Student_Class
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public class Student_Class : BaseModel
    {
        /// <summary>
        /// StudentId
        /// </summary>
        [Key]
        public int StudentId { get; set; }

        /// <summary>
        /// ClassId
        /// </summary>
        [Key]
        public int ClassId { get; set; }

        /// <summary>
        /// SemesterId
        /// </summary>
        [Key]
        public int SemesterId { get; set; }

        /// <summary>
        /// SchoolYearId
        /// </summary>
        [Key]
        public int SchoolYearId { get; set; }
    }
}