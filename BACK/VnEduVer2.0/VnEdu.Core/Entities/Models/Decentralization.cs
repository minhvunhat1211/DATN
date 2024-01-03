using System.ComponentModel.DataAnnotations;

namespace VnEdu.Core.Entities.Models
{
    /// <summary>
    /// Information of Decentralization
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public class Decentralization
    {
        /// <summary>
        /// DecentralizationId
        /// </summary>
        [Key]
        public int DecentralizationId { get; set; }

        /// <summary>
        /// DecentralizationName
        /// </summary>
        public string DecentralizationName { get; set; } = string.Empty;

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}