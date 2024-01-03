using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;

namespace VnEdu.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Information of IDetailedTableScoreRepository
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface IDetailedTableScoreRepository
    {
        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="detailedTableScore">DetailedTableScore</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(02/01/2023)
        public Task<OperationResult<bool>> Insert(DetailedTableScore detailedTableScore);

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="detailedTableScoreId">DetailedTableScoreId</param>
        /// <param name="detailedTableScore">DetailedTableScore</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<bool>> Update(int detailedTableScoreId, DetailedTableScore detailedTableScore);

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="detailedTableScoreId">DetailedTableScoreId</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<object>> GetById(int detailedTableScoreId);
    }
}