using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Results;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;
using VnEdu.Core.Services.Commom;

namespace VnEdu.Core.Services
{
    /// <summary>
    /// Information of SemesterService
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    public class SemesterService : ISemesterService
    {
        private readonly ISemesterRepository _semesterRepository;
        private readonly ISchoolYearRepository _schoolYearRepository;

        public SemesterService(ISemesterRepository semesterRepository, ISchoolYearRepository schoolYearRepository)
        {
            _semesterRepository = semesterRepository;
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
            return await _semesterRepository.Delete(id);
        }

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="semester">Semester</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Insert(Semester semester)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. SemesterName is null
            if (string.IsNullOrWhiteSpace(semester.SemesterName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SemesterByNameNotEmpty);

                return result;
            }

            // Get semesterByName
            var semesterByName = await _semesterRepository.GetSemesterByName(semester.SemesterName, semester.SchoolYearId);

            // 2. semesterByName is already exist
            if (semesterByName.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SemesterByNameAlreadyExist);

                return result;
            }

            // 3. DateStart is null
            if (string.IsNullOrWhiteSpace(semester.DateStart.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SemesterByDateStart);

                return result;
            }

            // 4. DateEnd is null
            if (string.IsNullOrWhiteSpace(semester.DateEnd.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SemesterByDateEnd);

                return result;
            }

            // 5. SchoolYearIs is null
            if (semester.SchoolYearId == default(int))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ClassByNameNotEmptySchoolYear);

                return result;
            }

            // Get schoolYearById
            var schoolYearById = await _schoolYearRepository.GetById(semester.SchoolYearId);

            // 6. schoolYearById is null
            if (schoolYearById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.SchoolYearByIdNotFound, semester.SchoolYearId));

                return result;
            }

            result = await _semesterRepository.Insert(semester);

            return result;
        }

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="semester">Semester</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Update(int id, Semester semester)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. semesterName is null
            if (string.IsNullOrWhiteSpace(semester.SemesterName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SemesterByNameNotEmpty);

                return result;
            }

            /// Get semesterByName
            var semesterByName = await _semesterRepository.GetSemesterByName(semester.SemesterName, semester.SchoolYearId);

            // semesterByName is not null
            if (semesterByName.Data is not null)
            {
                // 2.1 semesterName is already exist
                if (semesterByName.Data.SemesterName.ToLower().Trim().Equals(semester.SemesterName.ToLower().Trim())
                    && semesterByName.Data.SemesterId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SemesterByNameAlreadyExist);

                    return result;
                }
            }

            // 3. DateStart is null
            if (string.IsNullOrWhiteSpace(semester.DateStart.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SemesterByDateStart);

                return result;
            }

            // 4. DateEnd is null
            if (string.IsNullOrWhiteSpace(semester.DateEnd.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SemesterByDateEnd);

                return result;
            }

            // 5. SchoolYearIs is null
            if (semester.SchoolYearId == default(int))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ClassByNameNotEmptySchoolYear);

                return result;
            }

            // Get schoolYearById
            var schoolYearById = await _schoolYearRepository.GetById(semester.SchoolYearId);

            // 6. schoolYearById is null
            if (schoolYearById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.SchoolYearByIdNotFound, semester.SchoolYearId));

                return result;
            }

            result = await _semesterRepository.Update(id, semester);

            return result;
        }
    }
}