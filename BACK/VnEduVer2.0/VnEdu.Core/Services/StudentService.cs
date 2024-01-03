using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Results;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;
using VnEdu.Core.Services.Commom;

namespace VnEdu.Core.Services
{
    /// <summary>
    /// Informatiion of StudentService
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Delete(int id)
        {
            return await _studentRepository.Delete(id);
        }

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="student">Student</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Insert(Student student)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. StudentCode is null
            if (string.IsNullOrWhiteSpace(student.StudentCode))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByCodeNotEmpty);

                return result;
            }

            // Get studentByCode
            var studentByCode = await _studentRepository.GetStudentByCode(student.StudentCode);

            // 2. studentByCode is already exist
            if (studentByCode.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByCodeAlreadyExist);

                return result;
            }    

            // 3. FullName is null
            if (string.IsNullOrWhiteSpace(student.FullName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByNameNotEmpty);

                return result;
            }

            // 4. Gender is null
            if (student.Gender < 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByGenderNotEmpty);

                return result;
            }

            // 5.1 DateOfBirth is null
            if (student.DateOfBirth == default(DateTime))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByDateOfBirthNotEmpty);

                return result;
            }

            // 5.2 DateOfBirth <= DateTime.UtcNow
            if ((DateTime.UtcNow - student.DateOfBirth).TotalSeconds < 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByDateUtcNowOfBirthNotEmpty);

                return result;
            }

            // 6. PhoneNumber is null
            if (string.IsNullOrWhiteSpace(student.PhoneNumber))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByPhoneNumberNotEmpty);

                return result;
            }

            // Get studentByPhoneNumber
            var studentByPhoneNumber = await _studentRepository.GetStudentByPhoneNumber(student.PhoneNumber);

            // 7. studentByPhoneNumber is already exist
            if (studentByPhoneNumber.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByPhoneNumberAlreadyExist);

                return result;
            }

            // 8. Email is null
            if (string.IsNullOrWhiteSpace(student.Email))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByEmailNotEmpty);

                return result;
            }

            // Get studentByEmail
            var studentByEmail = await _studentRepository.GetStudentByEmail(student.Email);

            // 9. studentByEmail is already exist
            if (studentByEmail.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByEmailAlreadyExist);

                return result;
            }

            // 10. NumberCard is null
            if (string.IsNullOrWhiteSpace(student.NumberCard))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByNumberCardNotEmpty);

                return result;
            }

            // Get studentByNumberCard
            var studentByNumberCard = await _studentRepository.GetStudentByNumberCard(student.NumberCard);

            // 11. studentByNumberCard is not null
            if (studentByNumberCard.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByNumberCardAlreadyExist);

                return result;
            }

            // 12. AddressCurent is null
            if (string.IsNullOrWhiteSpace(student.AddressCurent))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByAddressCurentNotEmpty);

                return result;
            }

            // 13. Address is null
            if (string.IsNullOrWhiteSpace(student.Address))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByAddressNotEmpty);

                return result;
            }

            result = await _studentRepository.Insert(student);

            return result;
        }

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="student">Student</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Update(int id, Student student)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. StudentCode is null
            if (string.IsNullOrWhiteSpace(student.StudentCode))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByCodeNotEmpty);

                return result;
            }

            // Get studentByCode
            var studentByCode = await _studentRepository.GetStudentByCode(student.StudentCode);

            // 2. studentByCode is not null
            if (studentByCode.Data is not null)
            {
                // 2.1 StudentCode is already exist
                if (studentByCode.Data.StudentCode.ToLower().Trim().Equals(student.StudentCode.ToLower().Trim())
                    && studentByCode.Data.StudentId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByCodeAlreadyExist);

                    return result;
                }
            }

            // 3. FullName is null
            if (string.IsNullOrWhiteSpace(student.FullName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByNameNotEmpty);

                return result;
            }

            // 4. Gender is null
            if (student.Gender < 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByGenderNotEmpty);

                return result;
            }

            // 5.1 DateOfBirth is null
            if (student.DateOfBirth == default(DateTime))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByDateOfBirthNotEmpty);

                return result;
            }

            // 5.2 DateOfBirth <= DateTime.UtcNow
            if ((DateTime.UtcNow - student.DateOfBirth).TotalSeconds < 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByDateUtcNowOfBirthNotEmpty);

                return result;
            }

            // 6. PhoneNumber is null
            if (string.IsNullOrWhiteSpace(student.PhoneNumber))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByPhoneNumberNotEmpty);

                return result;
            }

            // Get studentByPhoneNumber
            var studentByPhoneNumber = await _studentRepository.GetStudentByPhoneNumber(student.PhoneNumber);

            // 7. studentByPhoneNumber is not null
            if (studentByPhoneNumber.Data is not null)
            {
                // 7.1 PhoneNumber is already exist
                if (studentByPhoneNumber.Data.PhoneNumber.ToLower().Trim().Equals(student.PhoneNumber.ToLower().Trim())
                    && studentByPhoneNumber.Data.StudentId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByPhoneNumberAlreadyExist);

                    return result;
                }
            }

            // 8. Email is null
            if (string.IsNullOrWhiteSpace(student.Email))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByEmailNotEmpty);

                return result;
            }

            // Get studentByEmail
            var studentByEmail = await _studentRepository.GetStudentByEmail(student.Email);

            // 9. studentByEmail is not null
            if (studentByEmail.Data is not null)
            {
                // 9.1 Email is already exist
                if (studentByEmail.Data.Email.ToLower().Trim().Equals(student.Email.ToLower().Trim())
                    && studentByEmail.Data.StudentId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByEmailAlreadyExist);

                    return result;
                }
            }

            // 10. NumberCard is null
            if (string.IsNullOrWhiteSpace(student.NumberCard))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByNumberCardNotEmpty);

                return result;
            }

            // Get studentByNumberCard
            var studentByNumberCard = await _studentRepository.GetStudentByNumberCard(student.NumberCard);

            // 11. studentByNumberCard is not null
            if (studentByNumberCard.Data is not null)
            {
                // 11.1 NumberCard is already exist
                if (studentByNumberCard.Data.NumberCard.ToLower().Trim().Equals(student.NumberCard.ToLower().Trim())
                    && studentByNumberCard.Data.StudentId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByNumberCardAlreadyExist);

                    return result;
                }
            }

            // 12. AddressCurent is null
            if (string.IsNullOrWhiteSpace(student.AddressCurent))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.TeacherByAddressCurentNotEmpty);

                return result;
            }

            // 10. Address is null
            if (string.IsNullOrWhiteSpace(student.Address))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.StudentByAddressNotEmpty);

                return result;
            }

            result = await _studentRepository.Update(id, student);

            return result;
        }
    }
}