using System.ComponentModel.DataAnnotations;

namespace VnEdu.Core.Entities.Models
{
    /// <summary>
    /// Information of Class
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public class Class : BaseModel
    {
        /// <summary>
        /// ClassId
        /// </summary>
        [Key]
        public int ClassId { get; set; }

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