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
    /// Information of Student_ClassRepository
    /// CreatedBy: MinhVN(24/12/2022)
    /// </summary>
    public class Student_ClassRepository : IStudent_ClassRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public Student_ClassRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// GetStudent_ClassById
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="classId">ClassId</param>
        /// <returns>Student_Class</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<Student_Class>> GetStudent_ClassById(int studentId, int schoolYearId, int semesterId,
            int classId)
        {
            var result = new OperationResult<Student_Class>();

            try
            {
                var student_class = await _dataContext.Student_Class
                    .FirstOrDefaultAsync(sc => sc.StudentId == studentId
                        && sc.ClassId == classId
                        && sc.SemesterId == semesterId
                        && sc.SchoolYearId == schoolYearId);

                // Check student_class is null
                if (student_class is null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.Student_ClassByNotFound);

                    return result;
                }

                result.Data = student_class;
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
        /// <param name="studentId">StudentId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        public async Task<OperationResult<bool>> Delete(int studentId, int classId, int semesterId, int schoolYearId)
        {
            var result = new OperationResult<bool>();

            try
            {
                var student_class = await _dataContext.Student_Class
                    .FirstOrDefaultAsync(sc => sc.StudentId == studentId
                        && sc.ClassId == classId 
                        && sc.SemesterId == semesterId
                        && sc.SchoolYearId == schoolYearId);

                // Check student_class is null
                if (student_class is null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.Student_ClassByNotFound);

                    return result;
                }

                _dataContext.Student_Class.Remove(student_class);
                await _dataContext.SaveChangesAsync();

                result.Data = true;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        public async Task<OperationResult<object>> GetClassByStudentSchoolYearSemester(int studentId, int schoolYearId, int semesterId)
        {
            var result = new OperationResult<object>();

            try
            {
                var query = from sc in _dataContext.Student_Class
                    join c in _dataContext.Class on sc.ClassId equals c.ClassId
                    where sc.StudentId == studentId && sc.SchoolYearId == schoolYearId && sc.SemesterId == semesterId
                    select new
                    {
                        sc.ClassId,
                        c.ClassName
                    };

                result.Data = await query.ToListAsync();

            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetPagingStudentClassByClassSemesterSchoolYear
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="classId">CLassId</param>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="pageIndex">PageIndex</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>PagingResult</returns>
        /// CreatedBy: MinhVN(25/12/2022)
        public async Task<OperationResult<PagingResult<object>>> GetPagingStudentClassByClassSemesterSchoolYear(int schoolYearId,
            int semesterId, int classId, string? valueFilter, int pageIndex, int pageSize)
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
                var student_ClasssPaging = new List<object>();
                var student_Classs = new List<object>();

                if (!string.IsNullOrWhiteSpace(valueFilter))
                {
                    var student_ClassDb = await (from sc in _dataContext.Student_Class
                        join st in _dataContext.Student on sc.StudentId equals st.StudentId
                        join c in _dataContext.Class on sc.ClassId equals c.ClassId
                        join sm in _dataContext.Semester on sc.SemesterId equals sm.SemesterId
                        join sy in _dataContext.SchoolYear on sc.SchoolYearId equals sy.SchoolYearId
                        select new
                        {
                            sc.StudentId,
                            st.StudentCode,
                            st.FullName,
                            st.Gender,
                            st.DateOfBirth,
                            st.PhoneNumber,
                            st.Email,
                            st.Address,
                            sc.ClassId,
                            c.ClassName,
                            sc.SemesterId,
                            sm.SemesterName,
                            sc.SchoolYearId,
                            sy.SchoolYearName
                        })
                        .Where(st => st.StudentCode.ToLower().Trim().Contains(valueFilter.ToLower().Trim())
                            || st.FullName.ToLower().Trim().Contains(valueFilter.ToLower().Trim()))
                        .Where(sc => sc.SchoolYearId == schoolYearId && sc.SemesterId == semesterId && sc.ClassId == classId)
                        .ToListAsync();

                    student_Classs = _mapper.Map<List<object>>(student_ClassDb);

                    student_ClasssPaging = student_Classs
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }
                else
                {
                    var student_ClasssDB = await (from sc in _dataContext.Student_Class
                        join st in _dataContext.Student on sc.StudentId equals st.StudentId
                        join c in _dataContext.Class on sc.ClassId equals c.ClassId
                        join sm in _dataContext.Semester on sc.SemesterId equals sm.SemesterId
                        join sy in _dataContext.SchoolYear on sc.SchoolYearId equals sy.SchoolYearId
                        select new
                        {
                            sc.StudentId,
                            st.StudentCode,
                            st.FullName,
                            st.Gender,
                            st.DateOfBirth,
                            st.PhoneNumber,
                            st.Email,
                            st.Address,
                            sc.ClassId,
                            c.ClassName,
                            sc.SemesterId,
                            sm.SemesterName,
                            sc.SchoolYearId,
                            sy.SchoolYearName
                        })
                        .Where(sc => sc.SchoolYearId == schoolYearId && sc.SemesterId == semesterId && sc.ClassId == classId)
                        .ToListAsync();

                    student_Classs = _mapper.Map<List<object>>(student_ClasssDB);

                    student_ClasssPaging = student_Classs
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }

                var toTalRecord = student_Classs.Count;
                var toTalPage = (toTalRecord % pageSize) == 0 ? ((int)toTalRecord / (int)pageSize) : ((int)toTalRecord / (int)pageSize + 1);

                var pagingResult = new PagingResult<object>()
                {
                    ToTalPage = toTalPage,
                    ToTalRecord = toTalRecord,
                    Data = student_ClasssPaging
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
        /// GetAllStudentStudent_Class
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="classId">ClassId</param>
        /// <returns>List object</returns>
        /// CreatedBy: MinhVN(25/12/2022)
        public async Task<OperationResult<IEnumerable<object>>> GetAllStudentStudent_Class(int schoolYearId, int semesterId,
            int classId)
        {
            var result = new OperationResult<IEnumerable<object>>();

            try
            {
                var student_ClassAllStudent = await (from sc in _dataContext.Student_Class
                    join st in _dataContext.Student on sc.StudentId equals st.StudentId
                    join c in _dataContext.Class on sc.ClassId equals c.ClassId
                    join sm in _dataContext.Semester on sc.SemesterId equals sm.SemesterId
                    join sy in _dataContext.SchoolYear on sc.SchoolYearId equals sy.SchoolYearId
                    select new 
                    {
                        sc.StudentId,
                        st.StudentCode,
                        st.FullName,
                        st.Gender,
                        st.DateOfBirth,
                        st.PhoneNumber,
                        st.Email,
                        st.Address,
                        sc.ClassId,
                        c.ClassName,
                        sc.SemesterId,
                        sm.SemesterName,
                        sc.SchoolYearId,
                        sy.SchoolYearName
                    })
                    .Where(sc => sc.SchoolYearId == schoolYearId && sc.SemesterId == semesterId && sc.ClassId == classId)
                    .ToListAsync();

                result.Data = student_ClassAllStudent;
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
        /// <param name="student_Class">Student_Class</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(25/12/2022)
        public async Task<OperationResult<bool>> Insert(Student_Class student_Class)
        {
            var result = new OperationResult<bool>();

            try
            {
                var student_ClassNew = new Student_Class()
                {
                   StudentId = student_Class.StudentId,
                   ClassId = student_Class.ClassId,
                   SemesterId = student_Class.SemesterId,
                   SchoolYearId = student_Class.SchoolYearId,
                   CreatedDate = DateTime.UtcNow,
                   ModifiedDate = DateTime.UtcNow,
                   CreatedBy = student_Class.CreatedBy
                };

                _dataContext.Student_Class.Add(student_ClassNew);
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
        /// GetStudent_ClassByStudentSchoolYearSemester
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <returns>Student_Class</returns>
        /// CreatedBy: MinhVN(27/03/2023)
        public async Task<OperationResult<Student_Class>> GetStudent_ClassByStudentSchoolYearSemester(int studentId, int schoolYearId, int semesterId)
        {
            var result = new OperationResult<Student_Class>();

            try
            {
                var student_class = await _dataContext.Student_Class
                    .FirstOrDefaultAsync(sc => sc.StudentId == studentId
                        && sc.SemesterId == semesterId
                        && sc.SchoolYearId == schoolYearId);

                // Check student_class is null
                if (student_class is null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.Student_ClassByNotFound);

                    return result;
                }

                result.Data = student_class;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }
    }
}