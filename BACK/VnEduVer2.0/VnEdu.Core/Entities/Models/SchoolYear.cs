using System.ComponentModel.DataAnnotations;

namespace VnEdu.Core.Entities.Models
{
    /// <summary>
    /// Information of SchoolYear
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public class SchoolYear : BaseModel
    {
        /// <summary>
        /// SchoolYearId
        /// </summary>
        [Key]
        public int SchoolYearId { get; set; }

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