using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Options;

namespace VnEdu.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Information of ITeacher_ClassRepository
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface ITeacher_ClassRepository
    {
        /// <summary>
        /// GetAllTeacher_Class
        /// </summary>
        /// <param name="schoolYearId">schoolYearId</param>
        /// <param name="semesterId">semesterId</param>
        /// <param name="classId">classId</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<IEnumerable<object>>> GetAllTeacher_Class(int schoolYearId, int semesterId, int classId);

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

        /// <summary>
        /// GetTeacherClassById
        /// </summary>
        /// <param name="schoolYearId">schoolYearId</param>
        /// <param name="semesterId">semesterId</param>
        /// <param name="classId">classId</param>
        /// <param name="teacherId">TeacherId</param>
        /// <returns>Teacher_Class</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<Teacher_Class>> GetTeacherClassById(int schoolYearId, int semesterId, int classId, int teacherId);

        /// <summary>
        /// GetPagingTeacherClassByClassSemesterSchoolYear
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="pageIndex">PageIndex</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>PagingResult</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<PagingResult<object>>> GetPagingTeacherClassByClassSemesterSchoolYear(int schoolYearId, int semesterId,
            int classId, string? valueFilter, int pageIndex, int pageSize);
    }
}