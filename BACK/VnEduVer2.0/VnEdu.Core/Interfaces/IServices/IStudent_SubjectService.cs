using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;

namespace VnEdu.Core.Interfaces.IServices
{
    /// <summary>
    /// Information of IStudent_SubjectService
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface IStudent_SubjectService
    {
        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="student_Subject">Student_Subject</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<bool>> Insert(Student_Subject student_Subject);

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<bool>> Delete(int studentId, int subjectId, int semesterId, int schoolYearId);
    }
}