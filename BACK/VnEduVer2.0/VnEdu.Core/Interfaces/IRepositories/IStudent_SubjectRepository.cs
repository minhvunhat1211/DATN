using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Options;

namespace VnEdu.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Information of IStudent_SubjectRepository
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface IStudent_SubjectRepository
    {
        /// <summary>
        /// GetAllSubjectStudent_Subject
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<IEnumerable<object>>> GetAllSubjectStudent_Subject(int studentId, int semesterId, int schoolYearId);

        /// <summary>
        /// GetScoreByStudent
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="studentId">StudentId</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<IEnumerable<object>>> GetScoreByStudent(int schoolYearId, int semesterId, int studentId);

        /// <summary>
        /// GetScoreBySubject
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<IEnumerable<object>>> GetScoreBySubject(int schoolYearId, int semesterId, int subjectId);

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

        /// <summary>
        /// CheckStudentSubject
        /// </summary>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="studentId">StudentId</param>
        /// <returns>Student_Subject</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<Student_Subject>> GetStudent_SubjectById(int subjectId, int semesterId, int schoolYearId, int studentId);

        /// <summary>
        /// GetPagingStudentSubjectBySchoolYearSemesterSubjectClass
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="pageIndex">PageIndex</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>PagingResult</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<PagingResult<object>>> GetPagingStudentSubjectBySchoolYearSemesterSubjectClass(int schoolYearId,
            int semesterId, int subjectId, int classId, string? valueFilter, int pageIndex, int pageSize);
    }
}