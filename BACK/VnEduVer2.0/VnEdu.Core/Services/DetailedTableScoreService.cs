using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;

namespace VnEdu.Core.Services
{
    /// <summary>
    /// Information of DetailedTableScoreService
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    public class DetailedTableScoreService : IDetailedTableScoreService
    {
        private readonly IDetailedTableScoreRepository _detailedTableScoreRepository;

        public DetailedTableScoreService(IDetailedTableScoreRepository detailedTableScoreRepository)
        {
            _detailedTableScoreRepository = detailedTableScoreRepository;
        }

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="detailedTableScoreId">DetailedTableScoreId</param>
        /// <param name="detailedTableScore">DetailedTableScore</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Update(int detailedTableScoreId, DetailedTableScore detailedTableScore)
        {
            var result = new OperationResult<bool>();

            // Validate data
            // ToDo: Score to 0 from 10

            result = await _detailedTableScoreRepository.Update(detailedTableScoreId, detailedTableScore);

            return result;
        }
    }
}