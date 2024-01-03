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
    /// Information of Teacher_SubjectRepository
    /// CreatedBy: MinhVN(04/01/2023)
    /// </summary>
    public class Teacher_SubjectRepository : ITeacher_SubjectRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public Teacher_SubjectRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="teacherId">TeacherId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(04/01/2023)
        public async Task<OperationResult<bool>> Delete(int teacherId, int subjectId, int semesterId, int schoolYearId)
        {
            var result = new OperationResult<bool>();

            try
            {
                var teacher_Subject = await _dataContext.Teacher_Subject
                    .FirstOrDefaultAsync(ts => ts.TeacherId == teacherId
                        && ts.SubjectId == subjectId
                        && ts.SemesterId == semesterId
                        && ts.SchoolYearId == schoolYearId);

                // Check teacher_Subject is null
                if (teacher_Subject is null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.Teacher_SubjectByNotFound);

                    return result;
                }

                _dataContext.Teacher_Subject.Remove(teacher_Subject);
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
        /// GetAllTeacher_Subject
        /// </summary>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>List object</returns>
        /// CreatedBy: MinhVN(04/01/2023)
        public async Task<OperationResult<IEnumerable<object>>> GetAllTeacher_Subject(int subjectId, int semesterId, int schoolYearId)
        {
            var result = new OperationResult<IEnumerable<object>>();

            try
            {
                var teacher_Subjects = await (from ts in _dataContext.Teacher_Subject
                    join t in _dataContext.Teacher on ts.TeacherId equals t.TeacherId
                    join sj in _dataContext.Subject on ts.SubjectId equals sj.SubjectId
                    join sm in _dataContext.Semester on ts.SemesterId equals sm.SemesterId
                    join sy in _dataContext.SchoolYear on ts.SchoolYearId equals sy.SchoolYearId
                    select new
                    {
                        ts.TeacherId,
                        t.TeacherCode,
                        t.FullName,
                        t.Gender,
                        t.DateOfBirth,
                        t.PhoneNumber,
                        t.Email,
                        t.Address,
                        ts.SubjectId,
                        sj.SubjectName,
                        ts.SemesterId,
                        sm.SemesterName,
                        ts.SchoolYearId,
                        sy.SchoolYearName
                    })
                    .Where(ts => ts.SchoolYearId == schoolYearId && ts.SemesterId == semesterId && ts.SubjectId == subjectId)
                    .ToListAsync();

                result.Data = teacher_Subjects;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetPagingTeacherSubjectByClassSemesterSchoolYear
        /// </summary>
        /// <param name="schoolYearId">SChoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="pageIndex">PageIndex</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>PagingResult</returns>
        /// CreatedBy: MinhVN(04/01/2023)
        public async Task<OperationResult<PagingResult<object>>> GetPagingTeacherSubjectByClassSemesterSchoolYear(int schoolYearId,
            int semesterId, int subjectId, string? valueFilter, int pageIndex, int pageSize)
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
                var teacher_SubjectPaging = new List<object>();
                var teacher_Subjects = new List<object>();

                if (!string.IsNullOrWhiteSpace(valueFilter))
                {
                    var teacher_SubjectDb = await (from ts in _dataContext.Teacher_Subject
                        join t in _dataContext.Teacher on ts.TeacherId equals t.TeacherId
                        join sj in _dataContext.Subject on ts.SubjectId equals sj.SubjectId
                        join sm in _dataContext.Semester on ts.SemesterId equals sm.SemesterId
                        join sy in _dataContext.SchoolYear on ts.SchoolYearId equals sy.SchoolYearId
                        select new
                        {
                            ts.TeacherId,
                            t.TeacherCode,
                            t.FullName,
                            t.Gender,
                            t.DateOfBirth,
                            t.PhoneNumber,
                            t.Email,
                            t.Address,
                            ts.SubjectId,
                            sj.SubjectName,
                            ts.SemesterId,
                            sm.SemesterName,
                            ts.SchoolYearId,
                            sy.SchoolYearName
                        })
                        .Where(ts => ts.TeacherCode.ToLower().Trim().Contains(valueFilter.ToLower().Trim())
                            || ts.FullName.ToLower().Trim().Contains(valueFilter.ToLower().Trim()))
                        .Where(ts => ts.SchoolYearId == schoolYearId && ts.SemesterId == semesterId && ts.SubjectId == subjectId)
                        .ToListAsync();

                    teacher_Subjects = _mapper.Map<List<object>>(teacher_SubjectDb);

                    teacher_SubjectPaging = teacher_Subjects
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }
                else
                {
                    var teacher_SubjectDB = await (from ts in _dataContext.Teacher_Subject
                        join t in _dataContext.Teacher on ts.TeacherId equals t.TeacherId
                        join sj in _dataContext.Subject on ts.SubjectId equals sj.SubjectId
                        join sm in _dataContext.Semester on ts.SemesterId equals sm.SemesterId
                        join sy in _dataContext.SchoolYear on ts.SchoolYearId equals sy.SchoolYearId
                        select new
                        {
                            ts.TeacherId,
                            t.TeacherCode,
                            t.FullName,
                            t.Gender,
                            t.DateOfBirth,
                            t.PhoneNumber,
                            t.Email,
                            t.Address,
                            ts.SubjectId,
                            sj.SubjectName,
                            ts.SemesterId,
                            sm.SemesterName,
                            ts.SchoolYearId,
                            sy.SchoolYearName
                        })
                        .Where(ts => ts.SchoolYearId == schoolYearId && ts.SemesterId == semesterId && ts.SubjectId == subjectId)
                        .ToListAsync();

                    teacher_Subjects = _mapper.Map<List<object>>(teacher_SubjectDB);

                    teacher_SubjectPaging = teacher_Subjects
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }

                var toTalRecord = teacher_Subjects.Count;
                var toTalPage = (toTalRecord % pageSize) == 0 ? ((int)toTalRecord / (int)pageSize) : ((int)toTalRecord / (int)pageSize + 1);

                var pagingResult = new PagingResult<object>()
                {
                    ToTalPage = toTalPage,
                    ToTalRecord = toTalRecord,
                    Data = teacher_SubjectPaging
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
        /// GetTeacherSubjectById
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="teacherId">TeacherId</param>
        /// <returns>Teacher_Subject</returns>
        /// CreatedBy: MinhVN(04/01/2023)
        public async Task<OperationResult<Teacher_Subject>> GetTeacherSubjectById(int schoolYearId, int semesterId, int subjectId, int teacherId)
        {
            var result = new OperationResult<Teacher_Subject>();

            try
            {
                var teacher_Subject = await _dataContext.Teacher_Subject
                    .FirstOrDefaultAsync(ts => ts.TeacherId == teacherId
                        && ts.SubjectId == subjectId
                        && ts.SemesterId == semesterId
                        && ts.SchoolYearId == schoolYearId);

                // Check teacher_Subject is null
                if (teacher_Subject is null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.Teacher_SubjectByNotFound);

                    return result;
                }

                result.Data = teacher_Subject;
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
        /// <param name="teacher_Subject">Teacher_Subject</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(04/01/2023)
        public async Task<OperationResult<bool>> Insert(Teacher_Subject teacher_Subject)
        {
            var result = new OperationResult<bool>();

            try
            {
                var teacher_SubjectNew = new Teacher_Subject()
                {
                    TeacherId = teacher_Subject.TeacherId,
                    SubjectId = teacher_Subject.SubjectId,
                    SemesterId = teacher_Subject.SemesterId,
                    SchoolYearId = teacher_Subject.SchoolYearId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    CreatedBy = teacher_Subject.CreatedBy
                };

                _dataContext.Teacher_Subject.Add(teacher_Subject);
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