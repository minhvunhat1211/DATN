using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Options;

namespace VnEdu.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Information of IStudent_ClassRepository
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface IStudent_ClassRepository
    {
        /// <summary>
        /// GetAllStudentStudent_Class
        /// </summary>
        /// <param name="schoolYearId">schoolYearId</param>
        /// <param name="semesterId">semesterId</param>
        /// <param name="classId">classId</param>
        /// <returns>List object</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<IEnumerable<object>>> GetAllStudentStudent_Class(int schoolYearId, int semesterId, int classId);

        /// <summary>
        /// GetClassByStudentSchoolYearSemester
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <returns>List object</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<object>> GetClassByStudentSchoolYearSemester(int studentId, int schoolYearId, int semesterId);

        /// <summary>
        /// GetStudent_ClassById
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="classId">ClassId</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public Task<OperationResult<Student_Class>> GetStudent_ClassById(int studentId, int schoolYearId,
            int semesterId, int classId);

        /// <summary>
        /// GetStudent_ClassByStudentSchoolYearSemester
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(27/03/2023)
        public Task<OperationResult<Student_Class>> GetStudent_ClassByStudentSchoolYearSemester(int studentId, int schoolYearId,
            int semesterId);

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

        /// <summary>
        /// GetPagingStudentClassByClassSemesterSchoolYear
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="pageIndex">PageIndex</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>PagingResult</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<PagingResult<object>>> GetPagingStudentClassByClassSemesterSchoolYear(int schoolYearId, int semesterId,
            int classId, string? valueFilter, int pageIndex, int pageSize);
    }
}