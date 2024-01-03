using AutoMapper;
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
    /// Information of StudentRepository
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>s
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public StudentRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreateBy: MinhVN(24/12/2022)
        public async Task<OperationResult<bool>> Delete(int id)
        {
            var result = new OperationResult<bool>();

            try
            {
                var studentById = await _dataContext.Student.FirstOrDefaultAsync(s => s.StudentId == id);

                // Check studentById is null
                if (studentById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.StudentByIdNotFound, id));

                    return result;
                }

                _dataContext.Student.Remove(studentById);
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
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<IEnumerable<object>>> GetAll()
        {
            var result = new OperationResult<IEnumerable<object>>();

            try
            {
                var students = await _dataContext.Student
                  .Select(s => new
                  {
                     s.StudentId,
                     s.StudentCode,
                     s.FullName,
                     s.Gender,
                     s.DateOfBirth,
                     s.PhoneNumber,
                     s.Email,
                     s.NumberCard,
                     s.AddressCurent,
                     s.Address
                  })
                  .ToListAsync();

                result.Data = students;
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
        /// CreatedBy: MinhVN(24/12/2022)
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
                var studentsPaging = new List<object>();
                var students = new List<object>();

                if (!string.IsNullOrWhiteSpace(valueFilter))
                {
                    var studentDb = await _dataContext.Student
                        .Select(s => new
                        {
                            s.StudentId,
                            s.StudentCode,
                            s.FullName,
                            s.Gender,
                            s.DateOfBirth,
                            s.PhoneNumber,
                            s.Email,
                            s.NumberCard,
                            s.AddressCurent,
                            s.Address
                        })
                        .Where(s => s.StudentCode.ToLower().Trim().Contains(valueFilter.ToLower().Trim())
                            || s.FullName.ToLower().Trim().Contains(valueFilter.ToLower().Trim())
                            || s.PhoneNumber.ToLower().Trim().Contains(valueFilter.ToLower().Trim()))
                        .ToListAsync();

                    students = _mapper.Map<List<object>>(studentDb);

                    studentsPaging = students
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }
                else
                {
                    var studentDB = await _dataContext.Student
                        .Select(s => new
                        {
                            s.StudentId,
                            s.StudentCode,
                            s.FullName,
                            s.Gender,
                            s.DateOfBirth,
                            s.PhoneNumber,
                            s.Email,
                            s.NumberCard,
                            s.AddressCurent,
                            s.Address
                        })
                        .ToListAsync();

                    students = _mapper.Map<List<object>>(studentDB);

                    studentsPaging = students
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }

                var toTalRecord = students.Count;
                var toTalPage = (toTalRecord % pageSize) == 0 ? ((int)toTalRecord / (int)pageSize) : ((int)toTalRecord / (int)pageSize + 1);

                var pagingResult = new PagingResult<object>()
                {
                    ToTalPage = toTalPage,
                    ToTalRecord = toTalRecord,
                    Data = studentsPaging
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
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<object>> GetById(int id)
        {
            var result = new OperationResult<object>();

            try
            {
                var studentById = await _dataContext.Student
                    .Select(s => new
                    {
                        s.StudentId,
                        s.StudentCode,
                        s.FullName,
                        s.Gender,
                        s.DateOfBirth,
                        s.PhoneNumber,
                        s.Email,
                        s.NumberCard,
                        s.AddressCurent,
                        s.Address
                    })
                    .FirstOrDefaultAsync(s => s.StudentId == id);

                // Check studentById is null
                if (studentById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.StudentByIdNotFound, id));

                    return result;
                }

                result.Data = studentById;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetInformationStudentByEmail
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<object>> GetInformationStudentByEmail(string email)
        {
            var result = new OperationResult<object>();

            try
            {
                var studentByEmail = await _dataContext.Student
                    .Select(s => new
                    {
                        s.StudentId,
                        s.StudentCode,
                        s.FullName,
                        s.Gender,
                        s.DateOfBirth,
                        s.PhoneNumber,
                        s.Email,
                        s.NumberCard,
                        s.AddressCurent,
                        s.Address
                    })
                    .FirstOrDefaultAsync(s => s.Email.ToLower().Trim().Equals(email.ToLower().Trim()));

                // Check studentByEmail is null
                if (studentByEmail is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.StudentByEmailNotFound, email));

                    return result;
                }

                result.Data = studentByEmail;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetInformationStudentByPhoneNumber
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<object>> GetInformationStudentByPhoneNumber(string phoneNumber)
        {
            var result = new OperationResult<object>();

            try
            {
                var studentByPhoneNumber = await _dataContext.Student
                    .Select(s => new
                    {
                        s.StudentId,
                        s.StudentCode,
                        s.FullName,
                        s.Gender,
                        s.DateOfBirth,
                        s.PhoneNumber,
                        s.Email,
                        s.NumberCard,
                        s.AddressCurent,
                        s.Address
                    })
                    .FirstOrDefaultAsync(s => s.PhoneNumber.ToLower().Trim().Equals(phoneNumber.ToLower().Trim()));

                // Check studentByPhoneNumber is null
                if (studentByPhoneNumber is null)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(ConfigErrorMessageRepository.StudentByPhoneNumberNotFound, phoneNumber));

                    return result;
                }

                result.Data = studentByPhoneNumber;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetStudentByCode
        /// </summary>
        /// <param name="studentCode">StudentCode</param>
        /// <returns>Student</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<Student>> GetStudentByCode(string studentCode)
        {
            var result = new OperationResult<Student>();

            try
            {
                var studentByCode = await _dataContext.Student
                    .FirstOrDefaultAsync(s => s.StudentCode.ToLower().Trim().Equals(studentCode.ToLower().Trim()));

                // Check studentByCode is null
                if (studentByCode is null)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(ConfigErrorMessageRepository.StudentByCodeNotFound, studentCode));

                    return result;
                }

                result.Data = studentByCode;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetStudentByEmail
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Student</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<Student>> GetStudentByEmail(string email)
        {
            var result = new OperationResult<Student>();

            try
            {
                var studentByEmail = await _dataContext.Student
                    .FirstOrDefaultAsync(s => s.Email.ToLower().Trim().Equals(email.ToLower().Trim()));

                // Check studentByEmail is null
                if (studentByEmail is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.StudentByEmailNotFound, email));

                    return result;
                }

                result.Data = studentByEmail;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetStudentByNumberCard
        /// </summary>
        /// <param name="numberCard">NumberCard</param>
        /// <returns>Student</returns>
        /// CreatedBy: MinhVN(29/03/2023)
        public async Task<OperationResult<Student>> GetStudentByNumberCard(string numberCard)
        {
            var result = new OperationResult<Student>();

            try
            {
                var studentByNumberCard = await _dataContext.Student
                    .FirstOrDefaultAsync(s => s.NumberCard.ToLower().Trim().Equals(numberCard.ToLower().Trim()));

                // Check studentByNumberCard is null
                if (studentByNumberCard is null)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(ConfigErrorMessageRepository.StudentByNumberCardNotFound, numberCard));

                    return result;
                }

                result.Data = studentByNumberCard;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetStudentByPhoneNumber
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <returns>Student</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<Student>> GetStudentByPhoneNumber(string phoneNumber)
        {
            var result = new OperationResult<Student>();

            try
            {
                var studentByPhoneNumber = await _dataContext.Student
                    .FirstOrDefaultAsync(s => s.PhoneNumber.ToLower().Trim().Equals(phoneNumber.ToLower().Trim()));

                // Check studentByPhoneNumber is null
                if (studentByPhoneNumber is null)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(ConfigErrorMessageRepository.StudentByPhoneNumberNotFound, phoneNumber));

                    return result;
                }

                result.Data = studentByPhoneNumber;
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
        /// <param name="student">Student</param>
        /// <returns>Number record insert sucess</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<bool>> Insert(Student student)
        {
            var result = new OperationResult<bool>();

            try
            {
                var studentNew = new Student()
                {
                    StudentCode = student.StudentCode,
                    FullName = student.FullName,
                    Gender = student.Gender,
                    DateOfBirth = student.DateOfBirth,
                    PhoneNumber = student.PhoneNumber,
                    Email = student.Email,
                    NumberCard = student.NumberCard,
                    AddressCurent  =student.AddressCurent,
                    Address = student.Address,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    CreatedBy = student.CreatedBy
                };

                _dataContext.Student.Add(studentNew);
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
        /// <param name="student">Student</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<bool>> Update(int id, Student student)
        {
            var result = new OperationResult<bool>();

            try
            {
                var studentById = await _dataContext.Student.FirstOrDefaultAsync(s => s.StudentId == id);

                // Check studentById is null
                if (studentById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.StudentByIdNotFound, id));

                    return result;
                }

                studentById.StudentCode = student.StudentCode;
                studentById.FullName = student.FullName;
                studentById.Gender = student.Gender;
                studentById.DateOfBirth = student.DateOfBirth;
                studentById.PhoneNumber = student.PhoneNumber;
                studentById.Email = student.Email;
                studentById.NumberCard = student.NumberCard;
                studentById.AddressCurent = student.AddressCurent;
                studentById.Address = student.Address;
                studentById.ModifiedDate = DateTime.UtcNow;
                studentById.ModifiedBy = student.ModifiedBy;

                _dataContext.Student.Update(studentById);
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