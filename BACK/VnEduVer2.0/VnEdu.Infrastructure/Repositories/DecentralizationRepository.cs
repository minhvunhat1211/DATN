using Microsoft.EntityFrameworkCore;
using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Options;
using VnEdu.Core.Entities.Results;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Infrastructure.Commom;
using VnEdu.Infrastructure.Data;

namespace VnEdu.Infrastructure.Repositories
{
    /// <summary>
    /// Information of DecentralizationRepository
    /// CreatedBy: MinhVN(22/12/2022)
    /// </summary>
    public class DecentralizationRepository : IDecentralizationRepository
    {
        private readonly DataContext _dataContext;

        public DecentralizationRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// GetDecentralizationByName
        /// </summary>
        /// <param name="decentralizationName">DecentralizationName</param>
        /// <returns>Decentralization</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        public async Task<OperationResult<Decentralization>> GetDecentralizationByName(string decentralizationName)
        {
            var result = new OperationResult<Decentralization>();

            try
            {
                var decentralizationByName = await _dataContext.Decentralization.FirstOrDefaultAsync(d =>
                    d.DecentralizationName.ToLower().Trim().Equals(decentralizationName.ToLower().Trim()));

                // Check decentralizationByName is null
                if (decentralizationByName is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.ClassByNameNotFound, decentralizationName));

                    return result;
                }

                result.Data = decentralizationByName;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record Delete success</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        public async Task<OperationResult<bool>> Delete(int id)
        {
            var result = new OperationResult<bool>();

            try
            {
                var decentralizationById = await _dataContext.Decentralization.FirstOrDefaultAsync(d => d.DecentralizationId == id);

                // Check decentralizationById is null
                if (decentralizationById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.DecentralizationByIdNotFound, id));

                    return result;
                }

                _dataContext.Decentralization.Remove(decentralizationById);
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
        /// GetAll
        /// </summary>
        /// <returns>List object</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        public async Task<OperationResult<IEnumerable<object>>> GetAll()
        {
            var result = new OperationResult<IEnumerable<object>>();

            try
            {
                var decentralizations = await _dataContext.Decentralization.ToListAsync();

                result.Data = decentralizations;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetAllPaging
        /// </summary>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="pageIndex">PageIndex</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>PagingResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        public async Task<OperationResult<PagingResult<object>>> GetAllPaging(string? valueFilter, int pageIndex, int pageSize)
        {
            var result = new OperationResult<PagingResult<object>>();

            if (pageIndex <= 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.PageIndex);

                return result;
            }
            if (pageSize <= 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.PageSize);

                return result;
            }

            try
            {
                var decentralizationsPaging = new List<Decentralization>();
                var decentralizations = new List<Decentralization>();

                if (!string.IsNullOrWhiteSpace(valueFilter))
                {
                    decentralizations = await _dataContext.Decentralization
                        .Where(d => d.DecentralizationName.ToLower().Trim().Contains(valueFilter.ToLower().Trim())).ToListAsync();

                    decentralizationsPaging = decentralizations
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }
                else
                {
                    decentralizations = await _dataContext.Decentralization.ToListAsync();

                    decentralizationsPaging = decentralizations
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }

                var toTalRecord = decentralizations.Count;
                var toTalPage = (toTalRecord % pageSize) == 0 ? ((int)toTalRecord / (int)pageSize) : ((int)toTalRecord / (int)pageSize + 1);

                var pagingResult = new PagingResult<object>()
                {
                    ToTalPage = toTalPage,
                    ToTalRecord = toTalRecord,
                    Data = decentralizationsPaging
                };

                result.Data = pagingResult;

                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        public async Task<OperationResult<object>> GetById(int id)
        {
            var result = new OperationResult<object>();

            try
            {
                var decentralizationById = await _dataContext.Decentralization.FirstOrDefaultAsync(d => d.DecentralizationId == id);

                // Check decentralizationById is null
                if (decentralizationById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.DecentralizationByIdNotFound, id));

                    return result;
                }

                result.Data = decentralizationById;
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
        /// <param name="decentralization">Decentralization</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        public async Task<OperationResult<bool>> Insert(Decentralization decentralization)
        {
            var result = new OperationResult<bool>();

            try
            {
                var decentralizationNew = new Decentralization()
                {
                    DecentralizationName = decentralization.DecentralizationName,
                    Description = decentralization.Description
                };

                _dataContext.Decentralization.Add(decentralizationNew);
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
        /// Update a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="decentralization">Decentralization</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        public async Task<OperationResult<bool>> Update(int id, Decentralization decentralization)
        {
            var result = new OperationResult<bool>();

            try
            {
                var decentralizationById = await _dataContext.Decentralization.FirstOrDefaultAsync(d => d.DecentralizationId == id);

                // Check decentralizationById is null
                if (decentralizationById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.DecentralizationByIdNotFound, id));

                    return result;
                }

                decentralizationById.DecentralizationName = decentralization.DecentralizationName;
                decentralizationById.Description = decentralization.Description;

                _dataContext.Decentralization.Update(decentralizationById);
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