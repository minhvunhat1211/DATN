using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;

namespace VnEdu.Core.Interfaces.IServices
{
    /// <summary>
    /// Information of ITeacher_SubjectService
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface ITeacher_SubjectService
    {
        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="teacher_Subject">Teacher_Subject</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<bool>> Insert(Teacher_Subject teacher_Subject);

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="teacherId">TeacherId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<bool>> Delete(int teacherId, int subjectId, int semesterId, int schoolYearId);
    }
}