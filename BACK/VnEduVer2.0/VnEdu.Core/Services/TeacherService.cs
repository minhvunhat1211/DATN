using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Results;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;
using VnEdu.Core.Services.Commom;

namespace VnEdu.Core.Services
{
    /// <summary>
    /// Informatiion of TeacherService
    /// CreatedBy: MinhVN(24/12/2022)
    /// </summary>
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<bool>> Delete(int id)
        {
            return await _teacherRepository.Delete(id);
        }

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="teacher">Teacher</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<bool>> Insert(Teacher teacher)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. TeacherCode is null
            if (string.IsNullOrWhiteSpace(teacher.TeacherCode))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByCodeNotEmpty);

                return result;
            }

            // Get teacherByCode
            var teacherByCode = await _teacherRepository.GetTeacherByCode(teacher.TeacherCode);

            // 2. teacherByCode is already exist
            if (teacherByCode.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByCodeAlreadyExist);

                return result;
            }

            // 3. FullName is null
            if (string.IsNullOrWhiteSpace(teacher.FullName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByNameNotEmpty);

                return result;
            }

            // 4. Gender is null
            if (teacher.Gender < 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByGenderNotEmpty);

                return result;
            }

            // 5.1 DateOfBirth is null
            if (teacher.DateOfBirth == default(DateTime))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByDateOfBirthNotEmpty);

                return result;
            }

            // 5.2 DateOfBirth <= DateTime.UtcNow
            if ((DateTime.UtcNow - teacher.DateOfBirth).TotalSeconds < 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByDateUtcNowOfBirthNotEmpty);

                return result;
            }

            // 6. PhoneNumber is null
            if (string.IsNullOrWhiteSpace(teacher.PhoneNumber))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByPhoneNumberNotEmpty);

                return result;
            }

            // Get teacherByPhoneNumber
            var teacherByPhoneNumber = await _teacherRepository.GetTeacherByPhoneNumber(teacher.PhoneNumber);

            // 7. teacherByPhoneNumber is already exist
            if (teacherByPhoneNumber.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByPhoneNumberAlreadyExist);

                return result;
            }

            // 8. Email is null
            if (string.IsNullOrWhiteSpace(teacher.Email))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByEmailNotEmpty);

                return result;
            }

            // Get teacherByEmail
            var teacherByEmail = await _teacherRepository.GetTeacherByEmail(teacher.Email);

            // 9. teacherByEmail is already exist
            if (teacherByEmail.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByEmailAlreadyExist);

                return result;
            }

            // 10. NumberCard is null
            if (string.IsNullOrWhiteSpace(teacher.NumberCard))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByNumberCardNotEmpty);

                return result;
            }

            // Get teacherByNumberCard
            var teacherByNumberCard = await _teacherRepository.GetTeacherByNumberCard(teacher.NumberCard);

            // 11. teacherByNumberCard is not null
            if (teacherByNumberCard.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByNumberCardAlreadyExist);

                return result;
            }

            // 12. AddressCurent is null
            if (string.IsNullOrWhiteSpace(teacher.AddressCurent))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByAddressCurentNotEmpty);

                return result;
            }

            // 13. Address is null
            if (string.IsNullOrWhiteSpace(teacher.Address))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByAddressNotEmpty);

                return result;
            }

            result = await _teacherRepository.Insert(teacher);

            return result;
        }

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="teacher">Teacher</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<bool>> Update(int id, Teacher teacher)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. TeacherCode is null
            if (string.IsNullOrWhiteSpace(teacher.TeacherCode))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByCodeNotEmpty);

                return result;
            }

            // Get teacherByCode
            var teacherByCode = await _teacherRepository.GetTeacherByCode(teacher.TeacherCode);

            // 2. teacherByCode is not null
            if (teacherByCode.Data is not null)
            {
                // 2.1 TeacherCode is already exist
                if (teacherByCode.Data.TeacherCode.ToLower().Trim().Equals(teacher.TeacherCode.ToLower().Trim())
                    && teacherByCode.Data.TeacherId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByCodeAlreadyExist);

                    return result;
                }
            }

            // 3. FullName is null
            if (string.IsNullOrWhiteSpace(teacher.FullName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByNameNotEmpty);

                return result;
            }

            // 4. Gender is null
            if (teacher.Gender < 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByGenderNotEmpty);

                return result;
            }

            // 5.1 DateOfBirth is null
            if (teacher.DateOfBirth == default(DateTime))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByDateOfBirthNotEmpty);

                return result;
            }

            // 5.2 DateOfBirth <= DateTime.UtcNow
            if ((DateTime.UtcNow - teacher.DateOfBirth).TotalSeconds < 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByDateUtcNowOfBirthNotEmpty);

                return result;
            }

            // 6. PhoneNumber is null
            if (string.IsNullOrWhiteSpace(teacher.PhoneNumber))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByPhoneNumberNotEmpty);

                return result;
            }

            // Get teacherByPhoneNumber
            var teacherByPhoneNumber = await _teacherRepository.GetTeacherByPhoneNumber(teacher.PhoneNumber);

            // 7. teacherByPhoneNumber is not null
            if (teacherByPhoneNumber.Data is not null)
            {
                // 7.1 PhoneNumber is already exist
                if (teacherByPhoneNumber.Data.PhoneNumber.ToLower().Trim().Equals(teacher.PhoneNumber.ToLower().Trim())
                    && teacherByPhoneNumber.Data.TeacherId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByPhoneNumberAlreadyExist);

                    return result;
                }
            }

            // 8. Email is null
            if (string.IsNullOrWhiteSpace(teacher.Email))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByEmailNotEmpty);

                return result;
            }

            // Get teacherByEmail
            var teacherByEmail = await _teacherRepository.GetTeacherByEmail(teacher.Email);

            // 9. teacherByEmail is not null
            if (teacherByEmail.Data is not null)
            {
                // 9.1 Email is already exist
                if (teacherByEmail.Data.Email.ToLower().Trim().Equals(teacher.Email.ToLower().Trim())
                    && teacherByEmail.Data.TeacherId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByEmailAlreadyExist);

                    return result;
                }
            }

            // 10. NumberCard is null
            if (string.IsNullOrWhiteSpace(teacher.NumberCard))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByNumberCardNotEmpty);

                return result;
            }

            // Get teacherByNumberCard
            var teacherByNumberCard = await _teacherRepository.GetTeacherByNumberCard(teacher.NumberCard);

            // 11. teacherByNumberCard is not null
            if (teacherByNumberCard.Data is not null)
            {
                // 11.1 NumberCard is already exist
                if (teacherByNumberCard.Data.NumberCard.ToLower().Trim().Equals(teacher.NumberCard.ToLower().Trim())
                    && teacherByNumberCard.Data.TeacherId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByNumberCardAlreadyExist);

                    return result;
                }
            }

            // 12. AddressCurent is null
            if (string.IsNullOrWhiteSpace(teacher.AddressCurent))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByAddressCurentNotEmpty);

                return result;
            }

            // 13. Address is null
            if (string.IsNullOrWhiteSpace(teacher.Address))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByAddressNotEmpty);

                return result;
            }

            result = await _teacherRepository.Update(id, teacher);

            return result;
        }
    }
}