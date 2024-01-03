using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VnEdu.Core.Entities.Models
{
    /// <summary>
    /// Information of Teacher_Subject
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public class Teacher_Subject : BaseModel
    {
        /// <summary>
        /// TeacherId
        /// </summary>
        public int TeacherId { get; set; }

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
