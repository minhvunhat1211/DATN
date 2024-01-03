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
    /// Information of TeacherRepository
    /// CreatedBy: MinhVN(24/12/2022)
    /// </summary>
    public class TeacherRepository : ITeacherRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public TeacherRepository(DataContext dataContext, IMapper mapper)
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
                var teacherById = await _dataContext.Teacher.FirstOrDefaultAsync(t => t.TeacherId == id);

                // Check teacherById is null
                if (teacherById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.TeacherByIdNotFound, id));

                    return result;
                }

                _dataContext.Teacher.Remove(teacherById);
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
                var teachers = await _dataContext.Teacher
                  .Select(t => new
                  {
                      t.TeacherId,
                      t.TeacherCode,
                      t.FullName,
                      t.Gender,
                      t.DateOfBirth,
                      t.PhoneNumber,
                      t.Email,
                      t.NumberCard,
                      t.AddressCurent,
                      t.Address
                  })
                  .ToListAsync();

                result.Data = teachers;
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
                var teachersPaging = new List<object>();
                var teachers = new List<object>();

                if (!string.IsNullOrWhiteSpace(valueFilter))
                {
                    var teacherDb = await _dataContext.Teacher
                        .Select(t => new
                        {
                            t.TeacherId,
                            t.TeacherCode,
                            t.FullName,
                            t.Gender,
                            t.DateOfBirth,
                            t.PhoneNumber,
                            t.Email,
                            t.NumberCard,
                            t.AddressCurent,
                            t.Address
                        })
                        .Where(t => t.TeacherCode.ToLower().Trim().Contains(valueFilter.ToLower().Trim())
                            || t.FullName.ToLower().Trim().Contains(valueFilter.ToLower().Trim())
                            || t.PhoneNumber.ToLower().Trim().Contains(valueFilter.ToLower().Trim()))
                        .ToListAsync();

                    teachers = _mapper.Map<List<object>>(teacherDb);

                    teachersPaging = teachers
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }
                else
                {
                    var teacherDB = await _dataContext.Teacher
                        .Select(t => new
                        {
                            t.TeacherId,
                            t.TeacherCode,
                            t.FullName,
                            t.Gender,
                            t.DateOfBirth,
                            t.PhoneNumber,
                            t.Email,
                            t.NumberCard,
                            t.AddressCurent,
                            t.Address
                        })
                        .ToListAsync();

                    teachers = _mapper.Map<List<object>>(teacherDB);

                    teachersPaging = teachers
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }

                var toTalRecord = teachers.Count;
                var toTalPage = (toTalRecord % pageSize) == 0 ? ((int)toTalRecord / (int)pageSize) : ((int)toTalRecord / (int)pageSize + 1);

                var pagingResult = new PagingResult<object>()
                {
                    ToTalPage = toTalPage,
                    ToTalRecord = toTalRecord,
                    Data = teachersPaging
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
                var teacherById = await _dataContext.Teacher
                    .Select(t => new
                    {
                        t.TeacherId,
                        t.TeacherCode,
                        t.FullName,
                        t.Gender,
                        t.DateOfBirth,
                        t.PhoneNumber,
                        t.Email,
                        t.NumberCard,
                        t.AddressCurent,
                        t.Address
                    })
                    .FirstOrDefaultAsync(t => t.TeacherId == id);

                // Check teacherById is null
                if (teacherById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.TeacherByIdNotFound, id));

                    return result;
                }

                result.Data = teacherById;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetInformationTeacherByEmail
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<object>> GetInformationTeacherByEmail(string email)
        {
            var result = new OperationResult<object>();

            try
            {
                var teacherByEmail = await _dataContext.Teacher
                    .Select(t => new
                    {
                        t.TeacherId,
                        t.TeacherCode,
                        t.FullName,
                        t.Gender,
                        t.DateOfBirth,
                        t.PhoneNumber,
                        t.Email,
                        t.NumberCard,
                        t.AddressCurent,
                        t.Address
                    })
                    .FirstOrDefaultAsync(t => t.Email.ToLower().Trim().Equals(email.ToLower().Trim()));

                // Check teacherByEmail is null
                if (teacherByEmail is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.TeacherByEmailNotFound, email));

                    return result;
                }

                result.Data = teacherByEmail;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetInformationTeacherByPhoneNumber
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<object>> GetInformationTeacherByPhoneNumber(string phoneNumber)
        {
            var result = new OperationResult<object>();

            try
            {
                var teacherByPhoneNumber = await _dataContext.Teacher
                    .Select(t => new
                    {
                        t.TeacherId,
                        t.TeacherCode,
                        t.FullName,
                        t.Gender,
                        t.DateOfBirth,
                        t.PhoneNumber,
                        t.Email,
                        t.NumberCard,
                        t.AddressCurent,
                        t.Address
                    })
                    .FirstOrDefaultAsync(t => t.PhoneNumber.ToLower().Trim().Equals(phoneNumber.ToLower().Trim()));

                // Check teacherByPhoneNumber is null
                if (teacherByPhoneNumber is null)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(ConfigErrorMessageRepository.TeacherByPhoneNumberNotFound, phoneNumber));

                    return result;
                }

                result.Data = teacherByPhoneNumber;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetTeacherByCode
        /// </summary>
        /// <param name="teacherCode">TeacherCode</param>
        /// <returns>Teacher</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<Teacher>> GetTeacherByCode(string teacherCode)
        {
            var result = new OperationResult<Teacher>();

            try
            {
                var teacherByCode = await _dataContext.Teacher
                    .FirstOrDefaultAsync(t => t.TeacherCode.ToLower().Trim().Equals(teacherCode.ToLower().Trim()));

                // Check teacherByCode is null
                if (teacherByCode is null)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(ConfigErrorMessageRepository.TeacherByCodeNotFound, teacherCode));

                    return result;
                }

                result.Data = teacherByCode;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetTeacherByEmail
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Teacher</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<Teacher>> GetTeacherByEmail(string email)
        {
            var result = new OperationResult<Teacher>();

            try
            {
                var teacherByEmail = await _dataContext.Teacher
                    .FirstOrDefaultAsync(t => t.Email.ToLower().Trim().Equals(email.ToLower().Trim()));

                // Check teacherByEmail is null
                if (teacherByEmail is null)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(ConfigErrorMessageRepository.TeacherByEmailNotFound, email));

                    return result;
                }

                result.Data = teacherByEmail;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetTeacherByNumberCard
        /// </summary>
        /// <param name="numberCard">NumberCard</param>
        /// <returns>Teacher</returns>
        /// CreatedBy: MinhVN(29/03/2023)
        public async Task<OperationResult<Teacher>> GetTeacherByNumberCard(string numberCard)
        {
            var result = new OperationResult<Teacher>();

            try
            {
                var teacherByNumberCard = await _dataContext.Teacher
                    .FirstOrDefaultAsync(t => t.NumberCard.ToLower().Trim().Equals(numberCard.ToLower().Trim()));

                // Check teacherByNumberCard is null
                if (teacherByNumberCard is null)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(ConfigErrorMessageRepository.TeacherByNumberCardNotFound, numberCard));

                    return result;
                }

                result.Data = teacherByNumberCard;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetTeacherByPhoneNumber
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <returns>Teacher</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<Teacher>> GetTeacherByPhoneNumber(string phoneNumber)
        {
            var result = new OperationResult<Teacher>();

            try
            {
                var teacherByPhoneNumber = await _dataContext.Teacher
                    .FirstOrDefaultAsync(t => t.PhoneNumber.ToLower().Trim().Equals(phoneNumber.ToLower().Trim()));

                // Check teacherByPhoneNumber is null
                if (teacherByPhoneNumber is null)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(ConfigErrorMessageRepository.TeacherByPhoneNumberNotFound, phoneNumber));

                    return result;
                }

                result.Data = teacherByPhoneNumber;
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
        /// <param name="teacher">Teacher</param>
        /// <returns>Number record insert sucess</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<bool>> Insert(Teacher teacher)
        {
            var result = new OperationResult<bool>();

            try
            {
                var teacherNew = new Teacher()
                {
                    TeacherCode = teacher.TeacherCode,
                    FullName = teacher.FullName,
                    Gender = teacher.Gender,
                    DateOfBirth = teacher.DateOfBirth,
                    PhoneNumber = teacher.PhoneNumber,
                    Email = teacher.Email,
                    NumberCard = teacher.NumberCard,
                    AddressCurent = teacher.AddressCurent,
                    Address = teacher.Address,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    CreatedBy = teacher.CreatedBy
                };

                _dataContext.Teacher.Add(teacherNew);
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
        /// <param name="teacher">Teacher</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<bool>> Update(int id, Teacher teacher)
        {
            var result = new OperationResult<bool>();

            try
            {
                var teacherById = await _dataContext.Teacher.FirstOrDefaultAsync(t => t.TeacherId == id);

                // Check teacherById is null
                if (teacherById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.TeacherByIdNotFound, id));

                    return result;
                }

                teacherById.TeacherCode = teacher.TeacherCode;
                teacherById.FullName = teacher.FullName;
                teacherById.Gender = teacher.Gender;
                teacherById.DateOfBirth = teacher.DateOfBirth;
                teacherById.PhoneNumber = teacher.PhoneNumber;
                teacherById.Email = teacher.Email;
                teacherById.NumberCard = teacher.NumberCard;
                teacherById.AddressCurent = teacher.AddressCurent;
                teacherById.Address = teacher.Address;
                teacherById.ModifiedDate = DateTime.UtcNow;
                teacherById.ModifiedBy = teacher.ModifiedBy;

                _dataContext.Teacher.Update(teacherById);
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