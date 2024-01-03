using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;

namespace VnEdu.Core.Interfaces.IServices
{
    /// <summary>
    /// Information of IDetailedTableScoreService
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface IDetailedTableScoreService
    {
        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="detailedTableScoreId">DetailedTableScoreId</param>
        /// <param name="detailedTableScore">DetailedTableScore</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<bool>> Update(int detailedTableScoreId, DetailedTableScore detailedTableScore);
    }
}