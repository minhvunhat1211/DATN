using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;

namespace VnEdu.Core.Interfaces.IServices
{
    /// <summary>
    /// Information of IStudent_ClassService
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface IStudent_ClassService
    {
        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="student_Class">Student_Class</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<bool>> Insert(Student_Class student_Class);

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<bool>> Delete(int studentId, int classId, int semesterId, int schoolYearId);
    }
}