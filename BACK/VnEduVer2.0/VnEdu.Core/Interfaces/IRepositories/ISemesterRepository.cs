using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;

namespace VnEdu.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Information of ISemesterRepository
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface ISemesterRepository : IBaseRepository<Semester>
    {
        /// <summary>
        /// GetSemesterByName
        /// </summary>
        /// <param name="semesterName">SemesterName</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>Semester</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<Semester>> GetSemesterByName(string semesterName, int schoolYearId);

        /// <summary>
        /// GetSemesterByName
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>Semester</returns>
        /// CreatedBy: MinhVN(26/03/2023)
        public Task<OperationResult<IEnumerable<object>>> GetBySchoolYearId(int schoolYearId);
    }
}