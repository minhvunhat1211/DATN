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
    /// Information of Student_SubjectRepository
    /// CreatedBy: MinhVN(02/01/2023)
    /// </summary>
    public class Student_SubjectRepository : IStudent_SubjectRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public Student_SubjectRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// GetStudent_SubjectById
        /// </summary>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="studentId">StudentId</param>
        /// <returns>Student_Subject</returns>
        /// CreatedBy: MinhVN(02/01/2023)
        public async Task<OperationResult<Student_Subject>> GetStudent_SubjectById(int subjectId, int semesterId, int schoolYearId,
            int studentId)
        {
            var result = new OperationResult<Student_Subject>();

            try
            {
                var student_subject = await _dataContext.Student_Subject
                    .FirstOrDefaultAsync(ss => ss.StudentId == studentId
                        && ss.SubjectId == subjectId
                        && ss.SemesterId == semesterId
                        && ss.SchoolYearId == schoolYearId);

                // Check student_subject is null
                if (student_subject is null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.Student_SubjectByIdNotFound);

                    return result;
                }

                result.Data = student_subject;
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
        /// <param name="subjectId">SubjectId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(02/01/2023)
        public async Task<OperationResult<bool>> Delete(int studentId, int subjectId, int semesterId, int schoolYearId)
        {
            var result = new OperationResult<bool>();

            try
            {
                var student_subject = await _dataContext.Student_Subject
                    .FirstOrDefaultAsync(ss => ss.StudentId == studentId
                        && ss.SubjectId == subjectId
                        && ss.SemesterId == semesterId
                        && ss.SchoolYearId == schoolYearId);

                // Check student_subject is null
                if (student_subject is null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.Student_SubjectByNotFound);

                    return result;
                }

                _dataContext.Student_Subject.Remove(student_subject);
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
        /// GetPagingStudentSubjectBySchoolYearSemesterSubjectClass
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="pageIndex">PageIndex</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>PagingResult</returns>
        /// CreatedBy: MinhVN(02/01/2023)
        public async Task<OperationResult<PagingResult<object>>> GetPagingStudentSubjectBySchoolYearSemesterSubjectClass
            (int schoolYearId, int semesterId, int subjectId, int classId, string? valueFilter, int pageIndex, int pageSize)
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
                var student_SubjectPaging = new List<object>();
                var student_Subjects = new List<object>();

                if (!string.IsNullOrWhiteSpace(valueFilter))
                {
                    var student_SubjectDb = await (from ss in _dataContext.Student_Subject
                        join st in _dataContext.Student on ss.StudentId equals st.StudentId
                        join sj in _dataContext.Subject on ss.SubjectId equals sj.SubjectId
                        join sm in _dataContext.Semester on ss.SemesterId equals sm.SemesterId
                        join sy in _dataContext.SchoolYear on ss.SchoolYearId equals sy.SchoolYearId
                        join d in _dataContext.DetailedTableScore on ss.DetailedTableScoreId equals d.DetailedTableScoreId
                        join sc in _dataContext.Student_Class on st.StudentId equals sc.StudentId
                        where sc.SemesterId == ss.SemesterId
                        select new
                        {
                            ss.DetailedTableScoreId,
                            ss.StudentId,
                            st.StudentCode,
                            st.FullName,
                            st.Gender,
                            st.DateOfBirth,
                            ss.SubjectId,
                            sj.SubjectName,
                            ss.SemesterId,
                            sm.SemesterName,
                            ss.SchoolYearId,
                            sy.SchoolYearName,
                            d.FirstOralScore,
                            d.SecondOralScore,
                            d.ThirdOralScore,
                            d.First15minutesScore,
                            d.Second15minutesScore,
                            d.Third15minutesScore,
                            d.OnePeriodScore,
                            d.FinalScore,
                            sc.ClassId
                        })
                        .Where(st => st.StudentCode.ToLower().Trim().Contains(valueFilter.ToLower().Trim())
                            || st.FullName.ToLower().Trim().Contains(valueFilter.ToLower().Trim()))
                        .Where(ss => ss.SchoolYearId == schoolYearId && ss.SemesterId == semesterId && ss.SubjectId == subjectId && ss.ClassId == classId)
                        .ToListAsync();

                    student_Subjects = _mapper.Map<List<object>>(student_SubjectDb);

                    student_SubjectPaging = student_Subjects
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }
                else
                {
                    var student_ClasssDB = await (from ss in _dataContext.Student_Subject
                        join st in _dataContext.Student on ss.StudentId equals st.StudentId
                        join sj in _dataContext.Subject on ss.SubjectId equals sj.SubjectId
                        join sm in _dataContext.Semester on ss.SemesterId equals sm.SemesterId
                        join sy in _dataContext.SchoolYear on ss.SchoolYearId equals sy.SchoolYearId
                        join sc in _dataContext.Student_Class on st.StudentId equals sc.StudentId
                        join d in _dataContext.DetailedTableScore on ss.DetailedTableScoreId equals d.DetailedTableScoreId
                        where sc.SemesterId == ss.SemesterId
                        select new
                        {
                            ss.DetailedTableScoreId,
                            ss.StudentId,
                            st.StudentCode,
                            st.FullName,
                            st.Gender,
                            st.DateOfBirth,
                            ss.SubjectId,
                            sj.SubjectName,
                            ss.SemesterId,
                            sm.SemesterName,
                            ss.SchoolYearId,
                            sy.SchoolYearName,
                            d.FirstOralScore,
                            d.SecondOralScore,
                            d.ThirdOralScore,
                            d.First15minutesScore,
                            d.Second15minutesScore,
                            d.Third15minutesScore,
                            d.OnePeriodScore,
                            d.FinalScore,
                            sc.ClassId
                        })
                        .Where(ss => ss.SchoolYearId == schoolYearId && ss.SemesterId == semesterId && ss.SubjectId == subjectId && ss.ClassId == classId)
                        .ToListAsync();

                    student_Subjects = _mapper.Map<List<object>>(student_ClasssDB);

                    student_SubjectPaging = student_Subjects
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }

                var toTalRecord = student_Subjects.Count;
                var toTalPage = (toTalRecord % pageSize) == 0 ? ((int)toTalRecord / (int)pageSize) : ((int)toTalRecord / (int)pageSize + 1);

                var pagingResult = new PagingResult<object>()
                {
                    ToTalPage = toTalPage,
                    ToTalRecord = toTalRecord,
                    Data = student_SubjectPaging
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
        /// GetScoreByStudent
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="studentId">StudentId</param>
        /// <returns>List object</returns>
        /// CreatedBy: MinhVN(02/01/2023)
        public async Task<OperationResult<IEnumerable<object>>> GetScoreByStudent(int schoolYearId, int semesterId, int studentId)
        {
            var result = new OperationResult<IEnumerable<object>>();

            try
            {
                var scoreOfStudent = await (from ss in _dataContext.Student_Subject
                    join st in _dataContext.Student on ss.StudentId equals st.StudentId
                    join sj in _dataContext.Subject on ss.SubjectId equals sj.SubjectId
                    join sm in _dataContext.Semester on ss.SemesterId equals sm.SemesterId
                    join sy in _dataContext.SchoolYear on ss.SchoolYearId equals sy.SchoolYearId
                    join d in _dataContext.DetailedTableScore on ss.DetailedTableScoreId equals d.DetailedTableScoreId
                    select new
                    {
                        ss.DetailedTableScoreId,
                        ss.StudentId,
                        st.FullName,
                        st.Gender,
                        st.DateOfBirth,
                        ss.SubjectId,
                        sj.SubjectName,
                        ss.SemesterId,
                        sm.SemesterName,
                        ss.SchoolYearId,
                        sy.SchoolYearName,
                        d.FirstOralScore,
                        d.SecondOralScore,
                        d.ThirdOralScore,
                        d.First15minutesScore,
                        d.Second15minutesScore,
                        d.Third15minutesScore,
                        d.OnePeriodScore,
                        d.FinalScore
                    })
                    .Where(ss => ss.SchoolYearId == schoolYearId && ss.SemesterId == semesterId && ss.StudentId == studentId)
                    .ToListAsync();

                result.Data = scoreOfStudent;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetScoreBySubject
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <returns>List object</returns>
        /// CreatedBy: MinhVN(02/01/2023)
        public async Task<OperationResult<IEnumerable<object>>> GetScoreBySubject(int schoolYearId, int semesterId, int subjectId)
        {
            var result = new OperationResult<IEnumerable<object>>();

            try
            {
                var scoreOfStudent = await(from ss in _dataContext.Student_Subject
                    join st in _dataContext.Student on ss.StudentId equals st.StudentId
                    join sj in _dataContext.Subject on ss.SubjectId equals sj.SubjectId
                    join sm in _dataContext.Semester on ss.SemesterId equals sm.SemesterId
                    join sy in _dataContext.SchoolYear on ss.SchoolYearId equals sy.SchoolYearId
                    join d in _dataContext.DetailedTableScore on ss.DetailedTableScoreId equals d.DetailedTableScoreId
                    select new
                    {
                        ss.DetailedTableScoreId,
                        ss.StudentId,
                        st.StudentCode,
                        st.FullName,
                        st.Gender,
                        st.DateOfBirth,
                        ss.SubjectId,
                        sj.SubjectName,
                        ss.SemesterId,
                        sm.SemesterName,
                        ss.SchoolYearId,
                        sy.SchoolYearName,
                        d.FirstOralScore,
                        d.SecondOralScore,
                        d.ThirdOralScore,
                        d.First15minutesScore,
                        d.Second15minutesScore,
                        d.Third15minutesScore,
                        d.OnePeriodScore,
                        d.FinalScore
                    })
                    .Where(ss => ss.SchoolYearId == schoolYearId && ss.SemesterId == semesterId && ss.SubjectId == subjectId)
                    .ToListAsync();

                result.Data = scoreOfStudent;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetAllSubjectStudent_Subject
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>List object</returns>
        /// CreatedBy: MinhVN(02/01/2023)
        public async Task<OperationResult<IEnumerable<object>>> GetAllSubjectStudent_Subject(int studentId, int semesterId, int schoolYearId)
        {
            var result = new OperationResult<IEnumerable<object>>();

            try
            {
                var student_SubjectAllSubject = await (from ss in _dataContext.Student_Subject
                    join st in _dataContext.Student on ss.StudentId equals st.StudentId
                    join sj in _dataContext.Subject on ss.SubjectId equals sj.SubjectId
                    join sm in _dataContext.Semester on ss.SemesterId equals sm.SemesterId
                    join sy in _dataContext.SchoolYear on ss.SchoolYearId equals sy.SchoolYearId
                    select new
                    {
                        ss.DetailedTableScoreId,
                        ss.StudentId,
                        st.FullName,
                        ss.SubjectId,
                        sj.SubjectName,
                        ss.SemesterId,
                        sm.SemesterName,
                        ss.SchoolYearId,
                        sy.SchoolYearName
                    })
                    .Where(ss => ss.SchoolYearId == schoolYearId && ss.SemesterId == semesterId && ss.StudentId == studentId)
                    .ToListAsync();

                result.Data = student_SubjectAllSubject;
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
        /// <param name="student_Subject">Student_Subject</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(02/01/2023)
        public async Task<OperationResult<bool>> Insert(Student_Subject student_Subject)
        {
            var result = new OperationResult<bool>();

            try
            {
                var student_SubjectNew = new Student_Subject()
                {
                    StudentId = student_Subject.StudentId,
                    SubjectId = student_Subject.SubjectId,
                    SemesterId = student_Subject.SemesterId,
                    SchoolYearId = student_Subject.SchoolYearId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    CreatedBy = student_Subject.CreatedBy
                };

                _dataContext.Student_Subject.Add(student_SubjectNew);
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