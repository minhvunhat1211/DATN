using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;

namespace VnEdu.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Information of ISchoolYearRepository
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface ISchoolYearRepository : IBaseRepository<SchoolYear>
    {
        /// <summary>
        /// GetSchoolYearByName
        /// </summary>
        /// <param name="schoolYearName">SchoolYearName</param>
        /// <returns>SchoolYear</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<SchoolYear>> GetSchoolYearByName(string schoolYearName);
    }
}