using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Results;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;
using VnEdu.Core.Services.Commom;

namespace VnEdu.Core.Services
{
    /// <summary>
    /// Information of SubjectService
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Delete(int id)
        {
            return await _subjectRepository.Delete(id);
        }

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="subject">Subject</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Insert(Subject subject)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. SubjectName is null
            if (string.IsNullOrWhiteSpace(subject.SubjectName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SubjectByNameNotEmpty);

                return result;
            }

            // Get subjectByName
            var subjectByName = await _subjectRepository.GetSubjectByName(subject.SubjectName);

            // 2. subjectByName is already exist
            if (subjectByName.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SubjectByNameAlreadyExist);

                return result;
            }

            // 3. PeriodsPerYear is null
            if (string.IsNullOrWhiteSpace(subject.PeriodsPerYear.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SubjectByPeriodsPerYearNotEmpty);

                return result;
            }

            // 4. Credit is null
            if (string.IsNullOrWhiteSpace(subject.Credit.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SubjectByCreditNotEmpty);

                return result;
            }

            result = await _subjectRepository.Insert(subject);

            return result;
        }

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="subject">Subject</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Update(int id, Subject subject)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. SubjectName is null
            if (string.IsNullOrWhiteSpace(subject.SubjectName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SubjectByNameNotEmpty);

                return result;
            }

            /// Get subjectByName
            var subjectByName = await _subjectRepository.GetSubjectByName(subject.SubjectName);

            // subjectByName is not null
            if (subjectByName.Data is not null)
            {
                // 2.1 SubjectName is already exist
                if (subjectByName.Data.SubjectName.ToLower().Trim().Equals(subject.SubjectName.ToLower().Trim())
                    && subjectByName.Data.SubjectId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SubjectByNameAlreadyExist);

                    return result;
                }
            }

            // 3. PeriodsPerYear is null
            if (string.IsNullOrWhiteSpace(subject.PeriodsPerYear.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SubjectByPeriodsPerYearNotEmpty);

                return result;
            }

            // 4. Credit is null
            if (string.IsNullOrWhiteSpace(subject.Credit.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SubjectByCreditNotEmpty);

                return result;
            }

            result = await _subjectRepository.Update(id, subject);

            return result;
        }
    }
}