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
    /// Information of Teacher_ClassRepository
    /// CreatedBy: MinhVN(03/01/2023)
    /// </summary>
    public class Teacher_ClassRepository : ITeacher_ClassRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public Teacher_ClassRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// GetTeacherClassById
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="teacherId">TeacherId</param>
        /// <returns>Teacher_Class</returns>
        /// CreatedBy: MinhVN(03/01/2023)
        public async Task<OperationResult<Teacher_Class>> GetTeacherClassById(int schoolYearId, int semesterId, int classId, int teacherId)
        {
            var result = new OperationResult<Teacher_Class>();

            try
            {
                var teacher_Class = await _dataContext.Teacher_Class
                    .FirstOrDefaultAsync(tc => tc.TeacherId == teacherId
                        && tc.ClassId == classId
                        && tc.SemesterId == semesterId
                        && tc.SchoolYearId == schoolYearId);

                // Check teacher_Class is null
                if (teacher_Class is null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.Teacher_ClassByNotFound);

                    return result;
                }

                result.Data = teacher_Class;
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
        /// <param name="teacherId">TeacherId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(04/01/2023)
        public async Task<OperationResult<bool>> Delete(int teacherId, int classId, int semesterId, int schoolYearId)
        {
            var result = new OperationResult<bool>();

            try
            {
                var teacher_Class = await _dataContext.Teacher_Class
                    .FirstOrDefaultAsync(tc => tc.TeacherId == teacherId
                        && tc.ClassId == classId
                        && tc.SemesterId == semesterId
                        && tc.SchoolYearId == schoolYearId);

                // Check teacher_Class is null
                if (teacher_Class is null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.Teacher_ClassByNotFound);

                    return result;
                }

                _dataContext.Teacher_Class.Remove(teacher_Class);
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
        /// GetPagingTeacherClassByClassSemesterSchoolYear
        /// </summary>
        /// <param name="schoolYearId">SChoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="pageIndex">PageIndex</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>PagingResult</returns>
        /// CreatedBy: MinhVN(04/01/2023)
        public async Task<OperationResult<PagingResult<object>>> GetPagingTeacherClassByClassSemesterSchoolYear(int schoolYearId,
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
                var teacher_ClassPaging = new List<object>();
                var teacher_Classs = new List<object>();

                if (!string.IsNullOrWhiteSpace(valueFilter))
                {
                    var teacher_ClassDb = await (from tc in _dataContext.Teacher_Class
                        join t in _dataContext.Teacher on tc.TeacherId equals t.TeacherId
                        join c in _dataContext.Class on tc.ClassId equals c.ClassId
                        join sm in _dataContext.Semester on tc.SemesterId equals sm.SemesterId
                        join sy in _dataContext.SchoolYear on tc.SchoolYearId equals sy.SchoolYearId
                        join ts in _dataContext.Teacher_Subject on new { tc.TeacherId, tc.SchoolYearId, tc.SemesterId } equals new { ts.TeacherId, ts.SchoolYearId, ts.SemesterId }
                        join s in _dataContext.Subject on ts.SubjectId equals s.SubjectId
                        select new
                        {
                            tc.TeacherId,
                            t.TeacherCode,
                            t.FullName,
                            t.Gender,
                            t.DateOfBirth,
                            t.PhoneNumber,
                            t.Email,
                            t.Address,
                            tc.ClassId,
                            c.ClassName,
                            tc.SemesterId,
                            sm.SemesterName,
                            tc.SchoolYearId,
                            sy.SchoolYearName,
                            ts.SubjectId,
                            s.SubjectName
                        })
                        .Where(tc => tc.TeacherCode.ToLower().Trim().Contains(valueFilter.ToLower().Trim())
                            || tc.FullName.ToLower().Trim().Contains(valueFilter.ToLower().Trim()))
                        .Where(sc => sc.SchoolYearId == schoolYearId && sc.SemesterId == semesterId && sc.ClassId == classId)
                        .ToListAsync();

                    teacher_Classs = _mapper.Map<List<object>>(teacher_ClassDb);

                    teacher_ClassPaging = teacher_Classs
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }
                else
                {
                    var teacher_ClassDB = await (from tc in _dataContext.Teacher_Class
                        join t in _dataContext.Teacher on tc.TeacherId equals t.TeacherId
                        join c in _dataContext.Class on tc.ClassId equals c.ClassId
                        join sm in _dataContext.Semester on tc.SemesterId equals sm.SemesterId
                        join sy in _dataContext.SchoolYear on tc.SchoolYearId equals sy.SchoolYearId
                        join ts in _dataContext.Teacher_Subject on new { tc.TeacherId, tc.SchoolYearId, tc.SemesterId } equals new { ts.TeacherId, ts.SchoolYearId, ts.SemesterId }
                        join s in _dataContext.Subject on ts.SubjectId equals s.SubjectId
                        select new
                        {
                            tc.TeacherId,
                            t.TeacherCode,
                            t.FullName,
                            t.Gender,
                            t.DateOfBirth,
                            t.PhoneNumber,
                            t.Email,
                            t.Address,
                            tc.ClassId,
                            c.ClassName,
                            tc.SemesterId,
                            sm.SemesterName,
                            tc.SchoolYearId,
                            sy.SchoolYearName,
                            ts.SubjectId,
                            s.SubjectName
                        })
                        .Where(sc => sc.SchoolYearId == schoolYearId && sc.SemesterId == semesterId && sc.ClassId == classId)
                        .ToListAsync();

                    teacher_Classs = _mapper.Map<List<object>>(teacher_ClassDB);

                    teacher_ClassPaging = teacher_Classs
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }

                var toTalRecord = teacher_Classs.Count;
                var toTalPage = (toTalRecord % pageSize) == 0 ? ((int)toTalRecord / (int)pageSize) : ((int)toTalRecord / (int)pageSize + 1);

                var pagingResult = new PagingResult<object>()
                {
                    ToTalPage = toTalPage,
                    ToTalRecord = toTalRecord,
                    Data = teacher_ClassPaging
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
        /// GetAllTeacher_Class
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="classId">ClassId</param>
        /// <returns>List object</returns>
        /// CreatedBy: MinhVN(04/01/2023)
        public async Task<OperationResult<IEnumerable<object>>> GetAllTeacher_Class(int schoolYearId, int semesterId, int classId)
        {
            var result = new OperationResult<IEnumerable<object>>();

            try
            {
                var teacher_ClassAll = await (from tc in _dataContext.Teacher_Class
                    join t in _dataContext.Teacher on tc.TeacherId equals t.TeacherId
                    join c in _dataContext.Class on tc.ClassId equals c.ClassId
                    join sm in _dataContext.Semester on tc.SemesterId equals sm.SemesterId
                    join sy in _dataContext.SchoolYear on tc.SchoolYearId equals sy.SchoolYearId
                    join ts in _dataContext.Teacher_Subject on new { tc.TeacherId, tc.SchoolYearId, tc.SemesterId } equals new { ts.TeacherId, ts.SchoolYearId, ts.SemesterId }
                    join s in _dataContext.Subject on ts.SubjectId equals s.SubjectId
                    select new
                    {
                        tc.TeacherId,
                        t.TeacherCode,
                        t.FullName,
                        t.Gender,
                        t.DateOfBirth,
                        t.PhoneNumber,
                        t.Email,
                        t.Address,
                        tc.ClassId,
                        c.ClassName,
                        tc.SemesterId,
                        sm.SemesterName,
                        tc.SchoolYearId,
                        sy.SchoolYearName,
                        ts.SubjectId,
                        s.SubjectName
                    })
                    .Where(sc => sc.SchoolYearId == schoolYearId && sc.SemesterId == semesterId && sc.ClassId == classId)
                    .ToListAsync();

                result.Data = teacher_ClassAll;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Insert a reocrd
        /// </summary>
        /// <param name="teacher_Class">Teacher_Class</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(04/01/2023)s
        public async Task<OperationResult<bool>> Insert(Teacher_Class teacher_Class)
        {
            var result = new OperationResult<bool>();

            try
            {   
                var teacher_ClassNew = new Teacher_Class()
                {
                    TeacherId = teacher_Class.TeacherId,
                    ClassId = teacher_Class.ClassId,
                    SemesterId = teacher_Class.SemesterId,
                    SchoolYearId = teacher_Class.SchoolYearId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    CreatedBy = teacher_Class.CreatedBy
                };

                _dataContext.Teacher_Class.Add(teacher_ClassNew);
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