using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Options;

namespace VnEdu.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Information of ITeacher_SubjectRepository
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface ITeacher_SubjectRepository
    {
        /// <summary>
        /// GetAllTeacher_Subject
        /// </summary>
        /// <param name="teacherId">TeacherId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>List object</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<IEnumerable<object>>> GetAllTeacher_Subject(int teacherId, int semesterId, int schoolYearId);

        /// <summary>
        /// GetTeacherSubjectById
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="teacherId">TeacherId</param>
        /// <returns>Teacher_Subject</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<Teacher_Subject>> GetTeacherSubjectById(int schoolYearId, int semesterId, int subjectId,
            int teacherId);

        /// <summary>
        /// GetPagingTeacherSubjectByClassSemesterSchoolYear
        /// </summary>
        /// <param 
        /// name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="pageIndex">PageIndex</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>PagingResult</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<PagingResult<object>>> GetPagingTeacherSubjectByClassSemesterSchoolYear(int schoolYearId,
            int semesterId, int subjectId, string? valueFilter, int pageIndex, int pageSize);

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