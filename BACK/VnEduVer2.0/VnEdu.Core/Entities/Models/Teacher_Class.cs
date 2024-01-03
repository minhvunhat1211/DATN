namespace VnEdu.Core.Entities.Models
{
    /// <summary>
    /// Informaton of Teacher_Class
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public class Teacher_Class : BaseModel
    {
        /// <summary>
        /// TeacherId
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// ClassId
        /// </summary>
        public int ClassId { get; set; }

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