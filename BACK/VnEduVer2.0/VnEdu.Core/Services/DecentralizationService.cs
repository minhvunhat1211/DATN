using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Results;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;
using VnEdu.Core.Services.Commom;

namespace VnEdu.Core.Services
{
    /// <summary>
    /// Information of DecentralizationService
    /// CreatedBy: MinhVN(22/12/2022)
    /// </summary>
    public class DecentralizationService : IDecentralizationService
    {
        private readonly IDecentralizationRepository _decentralizaRepository;

        public DecentralizationService(IDecentralizationRepository decentralizaRepository)
        {
            _decentralizaRepository = decentralizaRepository;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        public async Task<OperationResult<bool>> Delete(int id)
        {
            return await _decentralizaRepository.Delete(id);
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

            // Validate data

            // 1. DecentralizationName is null
            if (string.IsNullOrWhiteSpace(decentralization.DecentralizationName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.DecentralizationByNameNotEmpty);

                return result;
            }

            // Get decentralizationByName
            var decentralizationByName = await _decentralizaRepository.GetDecentralizationByName(decentralization.DecentralizationName);

            // 2. DecentralizationName is already exist
            if (decentralizationByName.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.DecentralizationByNameAlreadyExist);

                return result;
            }

            // 3. Description is null
            if (string.IsNullOrWhiteSpace(decentralization.Description))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.DecentralizationByDescriptionNotEmpty);

                return result;
            }

            result = await _decentralizaRepository.Insert(decentralization);

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

            // Validate data

            // 1. DecentralizationName is null
            if (string.IsNullOrWhiteSpace(decentralization.DecentralizationName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.DecentralizationByNameNotEmpty);

                return result;
            }

            // Get decentralizationByName
            var decentralizationByName = await _decentralizaRepository.GetDecentralizationByName(decentralization.DecentralizationName);

            // decentralizationByName is not null
            if (decentralizationByName.Data is not null)
            {
                // 2.2 DecentralizationName is already exist
                if (decentralizationByName.Data.DecentralizationName.ToLower().Trim().Equals(decentralization.DecentralizationName.ToLower().Trim())
                    && decentralizationByName.Data.DecentralizationId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.DecentralizationByNameAlreadyExist);

                    return result;
                }
            }

            // 3. Description is null
            if (string.IsNullOrWhiteSpace(decentralization.Description))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.DecentralizationByDescriptionNotEmpty);

                return result;
            }

            result = await _decentralizaRepository.Update(id, decentralization);

            return result;
        }
    }
}