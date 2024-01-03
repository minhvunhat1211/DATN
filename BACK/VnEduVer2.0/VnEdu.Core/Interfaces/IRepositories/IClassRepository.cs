using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;

namespace VnEdu.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Information of IClassRepository
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface IClassRepository : IBaseRepository<Class>
    {
        /// <summary>
        /// GetClassByName
        /// </summary>
        /// <param name="className">ClassName</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>Class</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<Class>> GetClassByName(string className, int schoolYearId);

        /// <summary>
        /// GetBySchoolYearId
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>List class</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<IEnumerable<object>>> GetBySchoolYearId(int schoolYearId);

        /// <summary>
        /// GetBySchoolYearTeacher
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="teacherId">TeacherId</param>
        /// <returns>Class</returns>
        /// CreatedBy: MinhVN(25/03/2023)
        public Task<OperationResult<Class>> GetBySchoolYearTeacher(int schoolYearId, int teacherId);
    }
}