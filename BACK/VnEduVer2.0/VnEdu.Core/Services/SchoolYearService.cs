using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Results;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;
using VnEdu.Core.Services.Commom;

namespace VnEdu.Core.Services
{
    /// <summary>
    /// Information of SchoolYearService
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    public class SchoolYearService : ISchoolYearService
    {
        private readonly ISchoolYearRepository _schoolYearRepository;

        public SchoolYearService(ISchoolYearRepository schoolYearRepository)
        {
            _schoolYearRepository = schoolYearRepository;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Delete(int id)
        {
            return await _schoolYearRepository.Delete(id);
        }

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="schoolYear">SchoolYear</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Insert(SchoolYear schoolYear)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. SchoolYearName is null
            if (string.IsNullOrWhiteSpace(schoolYear.SchoolYearName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SchoolYearByNameNotEmpty);

                return result;
            }

            // Get schoolYearByName
            var schoolYearByName = await _schoolYearRepository.GetSchoolYearByName(schoolYear.SchoolYearName);

            // 2. schoolYearByName is already exist
            if (schoolYearByName.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SchoolYearByNameAlreadyExist);

                return result;
            }

            // 3. DateStart is null
            if (string.IsNullOrWhiteSpace(schoolYear.DateStart.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SchoolYearByDateStart);

                return result;
            }

            // 4. DateEnd is null
            if (string.IsNullOrWhiteSpace(schoolYear.DateEnd.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SchoolYearByDateEnd);

                return result;
            }

            result = await _schoolYearRepository.Insert(schoolYear);

            return result;
        }

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="schoolYear">SchoolYear</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Update(int id, SchoolYear schoolYear)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. SchoolYearName is null
            if (string.IsNullOrWhiteSpace(schoolYear.SchoolYearName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SchoolYearByNameNotEmpty);

                return result;
            }

            /// Get schoolYearByName
            var schoolYearByName = await _schoolYearRepository.GetSchoolYearByName(schoolYear.SchoolYearName);

            // schoolYearByName is not null
            if (schoolYearByName.Data is not null)
            {
                // 2.1 SchoolYearName is already exist
                if (schoolYearByName.Data.SchoolYearName.ToLower().Trim().Equals(schoolYear.SchoolYearName.ToLower().Trim())
                    && schoolYearByName.Data.SchoolYearId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SchoolYearByNameAlreadyExist);

                    return result;
                }
            }

            // 3. DateStart is null
            if (string.IsNullOrWhiteSpace(schoolYear.DateStart.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SchoolYearByDateStart);

                return result;
            }

            // 4. DateEnd is null
            if (string.IsNullOrWhiteSpace(schoolYear.DateEnd.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SchoolYearByDateEnd);

                return result;
            }

            result = await _schoolYearRepository.Update(id, schoolYear);

            return result;
        }
    }
}