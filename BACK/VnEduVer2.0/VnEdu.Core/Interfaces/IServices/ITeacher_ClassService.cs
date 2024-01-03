using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;

namespace VnEdu.Core.Interfaces.IServices
{
    /// <summary>
    /// Information of ITeacher_ClassService
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface ITeacher_ClassService
    {
        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="teacher_Class">Teacher_Class</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<bool>> Insert(Teacher_Class teacher_Class);

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="teacherId">TeacherId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<bool>> Delete(int teacherId, int classId, int semesterId, int schoolYearId);
    }
}