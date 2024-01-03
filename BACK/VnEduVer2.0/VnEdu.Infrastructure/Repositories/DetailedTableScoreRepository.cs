using Microsoft.EntityFrameworkCore;
using System.Text;
using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Results;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Infrastructure.Commom;
using VnEdu.Infrastructure.Data;

namespace VnEdu.Infrastructure.Repositories
{
    /// <summary>
    /// Information of DetailedTableScoreRepository 
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    public class DetailedTableScoreRepository : IDetailedTableScoreRepository
    {
        private readonly DataContext _dataContext;

        public DetailedTableScoreRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="detailedTableScoreId">DetailedTableScoreId</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<object>> GetById(int detailedTableScoreId)
        {
            var result = new OperationResult<object>();

            try
            {
                var detailedTableScore = await _dataContext.DetailedTableScore
                    .Select(d => new
                    {
                        d.DetailedTableScoreId,
                        d.FirstOralScore,
                        d.SecondOralScore,
                        d.ThirdOralScore,
                        d.First15minutesScore,
                        d.Second15minutesScore,
                        d.Third15minutesScore,
                        d.OnePeriodScore,
                        d.FinalScore
                    })
                    .FirstOrDefaultAsync(d => d.DetailedTableScoreId == detailedTableScoreId);

                // Check detailedTableScore is null
                if (detailedTableScore is null)
                {
                    result.AddError(ErrorCode.NotFound, 
                        string.Format(ConfigErrorMessageRepository.DetailedTableScoreByIdNotFound, detailedTableScoreId));

                    return result;
                }

                result.Data = detailedTableScore;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
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

            try
            {
                var detailedTableScoreById = await _dataContext.DetailedTableScore
                    .FirstOrDefaultAsync(d => d.DetailedTableScoreId == detailedTableScoreId);

                // Check detailedTableScoreById is null
                if (detailedTableScoreById is null)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(ConfigErrorMessageRepository.DetailedTableScoreByIdNotFound, detailedTableScoreId));

                    return result;
                }

                detailedTableScoreById.FirstOralScore = detailedTableScore.FirstOralScore;
                detailedTableScoreById.SecondOralScore = detailedTableScore.SecondOralScore;
                detailedTableScoreById.ThirdOralScore = detailedTableScore.ThirdOralScore;
                detailedTableScoreById.First15minutesScore = detailedTableScore.First15minutesScore;
                detailedTableScoreById.Second15minutesScore = detailedTableScore.Second15minutesScore;
                detailedTableScoreById.Third15minutesScore = detailedTableScore.Third15minutesScore;
                detailedTableScoreById.OnePeriodScore = detailedTableScore.OnePeriodScore;
                detailedTableScoreById.FinalScore = detailedTableScore.FinalScore;
                detailedTableScoreById.ModifiedDate = DateTime.UtcNow;
                detailedTableScoreById.ModifiedBy = detailedTableScore.ModifiedBy;

                _dataContext.DetailedTableScore.Update(detailedTableScoreById);
                await _dataContext.SaveChangesAsync();

                result.Data = true;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="detailedTableScore">DetailedTableScore</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(02/01/2023)
        public async Task<OperationResult<bool>> Insert(DetailedTableScore detailedTableScore)
        {
            var result = new OperationResult<bool>();

            try
            {
                _dataContext.DetailedTableScore.Add(detailedTableScore);
                await _dataContext.SaveChangesAsync();

                result.Data = true;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }
    }
}