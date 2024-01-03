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
    /// Information of SemesterRepository
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    public class SemesterRepository : ISemesterRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public SemesterRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// GetSemesterByName
        /// </summary>
        /// <param name="semesterName">SemesterName</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>Semester</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<Semester>> GetSemesterByName(string semesterName, int schoolYearId)
        {
            var result = new OperationResult<Semester>();

            try
            {
                var semesterByName = await _dataContext.Semester.FirstOrDefaultAsync(s =>
                    s.SemesterName.ToLower().Trim().Equals(semesterName.ToLower().Trim())
                    && s.SchoolYearId == schoolYearId);

                // Check semesterByName is null
                if (semesterByName is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.SemesterByNameNotFound, semesterName));

                    return result;
                }

                result.Data = semesterByName;
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
        /// <param name="id">Id</param>
        /// <returns>Number record Delete success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Delete(int id)
        {
            var result = new OperationResult<bool>();

            try
            {
                var semesterById = await _dataContext.Semester.FirstOrDefaultAsync(s => s.SemesterId == id);

                // Check semesterById is null
                if (semesterById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.SemesterByIdNotFound, id));

                    return result;
                }

                _dataContext.Semester.Remove(semesterById);
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
        /// <returns>List object</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<IEnumerable<object>>> GetAll()
        {
            var result = new OperationResult<IEnumerable<object>>();

            try
            {
                var semesters = await (from s in _dataContext.Semester
                    join sy in _dataContext.SchoolYear on s.SchoolYearId equals sy.SchoolYearId
                    select new
                    {
                       s.SemesterId,
                       s.SemesterName,
                       s.DateStart,
                       s.DateEnd,
                       s.SchoolYearId,
                       sy.SchoolYearName
                    })
                    .ToListAsync();

                result.Data = semesters;
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
        /// CreatedBy: MinhVN(23/12/2022)
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
                var semestersPaging = new List<object>();
                var semesters = new List<object>();

                if (!string.IsNullOrWhiteSpace(valueFilter))
                {
                    var semesterDb = await (from s in _dataContext.Semester
                        join sy in _dataContext.SchoolYear on s.SchoolYearId equals sy.SchoolYearId
                        select new
                        {
                            s.SemesterId,
                            s.SemesterName,
                            s.DateStart,
                            s.DateEnd,
                            s.SchoolYearId,
                            sy.SchoolYearName
                        })
                        .Where(s => s.SemesterName.ToLower().Trim().Contains(valueFilter.ToLower().Trim()))
                        .ToListAsync();

                    semesters = _mapper.Map<List<object>>(semesterDb);

                    semestersPaging = semesters
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }
                else
                {
                    var semesterDB = await (from s in _dataContext.Semester
                        join sy in _dataContext.SchoolYear on s.SchoolYearId equals sy.SchoolYearId
                        select new
                        {
                            s.SemesterId,
                            s.SemesterName,
                            s.DateStart,
                            s.DateEnd,
                            s.SchoolYearId,
                            sy.SchoolYearName
                        })
                        .ToListAsync();

                    semesters = _mapper.Map<List<object>>(semesterDB);

                    semestersPaging = semesters
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }

                var toTalRecord = semesters.Count;
                var toTalPage = (toTalRecord % pageSize) == 0 ? ((int)toTalRecord / (int)pageSize) : ((int)toTalRecord / (int)pageSize + 1);

                var pagingResult = new PagingResult<object>()
                {
                    ToTalPage = toTalPage,
                    ToTalRecord = toTalRecord,
                    Data = semestersPaging
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
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<object>> GetById(int id)
        {
            var result = new OperationResult<object>();

            try
            {
                var semesterById = await (from s in _dataContext.Semester
                    join sy in _dataContext.SchoolYear on s.SchoolYearId equals sy.SchoolYearId
                    select new
                    {
                        s.SemesterId,
                        s.SemesterName,
                        s.DateStart,
                        s.DateEnd,
                        s.SchoolYearId,
                        sy.SchoolYearName
                    })
                    .FirstOrDefaultAsync(s => s.SemesterId == id);

                // Check semesterById is null
                if (semesterById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.SemesterByIdNotFound, id));

                    return result;
                }

                result.Data = semesterById;
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
        /// <param name="semester">Semester</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Insert(Semester semester)
        {
            var result = new OperationResult<bool>();

            try
            {
                var semesterNew = new Semester()
                {
                    SemesterName = semester.SemesterName,
                    DateStart = semester.DateStart,
                    DateEnd = semester.DateEnd,
                    SchoolYearId = semester.SchoolYearId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    CreatedBy = semester.CreatedBy
                };

                _dataContext.Semester.Add(semesterNew);
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
        /// <param name="semester">Semester</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Update(int id, Semester semester)
        {
            var result = new OperationResult<bool>();

            try
            {
                var semesterById = await _dataContext.Semester.FirstOrDefaultAsync(s => s.SemesterId == id);

                // Check semesterById is null
                if (semesterById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.SemesterByIdNotFound, id));

                    return result;
                }

                semesterById.SemesterName = semester.SemesterName;
                semesterById.DateStart = semester.DateStart;
                semesterById.DateEnd = semester.DateEnd;
                semesterById.SchoolYearId = semester.SchoolYearId;
                semesterById.ModifiedDate = DateTime.UtcNow;
                semesterById.ModifiedBy = semester.ModifiedBy;

                _dataContext.Semester.Update(semesterById);
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
        /// GetBySchoolYearId
        /// </summary>
        /// <param name="schoolYearId">schoolYearId</param>
        /// <returns>List semeter</returns>
        /// CreatedBy: MinhVN(26/02/2023)
        public async Task<OperationResult<IEnumerable<object>>> GetBySchoolYearId(int schoolYearId)
        {
            var result = new OperationResult<IEnumerable<object>>();

            try
            {
                var semesterBySchoolYearId = await (from s in _dataContext.Semester
                    join sy in _dataContext.SchoolYear on s.SchoolYearId equals sy.SchoolYearId
                    select new
                    {
                        s.SemesterId,
                        s.SemesterName,
                        s.SchoolYearId,
                        sy.SchoolYearName
                    })
                    .Where(c => c.SchoolYearId == schoolYearId)
                    .ToListAsync();

                result.Data = semesterBySchoolYearId;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }
    }
}