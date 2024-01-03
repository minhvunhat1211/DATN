namespace VnEdu.Core.Entities.Models
{
    /// <summary>
    /// Information of BaseModel
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// CreatedBy
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// CreatedDate
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// ModifiedBy
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// ModifiedDate
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}