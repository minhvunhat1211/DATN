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
    /// Information of SubjectRepository
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public SubjectRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// GetSubjectByName
        /// </summary>
        /// <param name="subjectName">SubjectName</param>
        /// <returns>Subject</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<Subject>> GetSubjectByName(string subjectName)
        {
            var result = new OperationResult<Subject>();

            try
            {
                var subjectByName = await _dataContext.Subject.FirstOrDefaultAsync(s =>
                    s.SubjectName.ToLower().Trim().Equals(subjectName.ToLower().Trim()));

                // Check subjectByName is null
                if (subjectByName is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.SubjectByNameNotFound, subjectName));

                    return result;
                }

                result.Data = subjectByName;
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
                var subjectById = await _dataContext.Subject.FirstOrDefaultAsync(s => s.SubjectId == id);

                // Check subjectById is null
                if (subjectById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.SubjectByIdNotFound, id));

                    return result;
                }

                _dataContext.Subject.Remove(subjectById);
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
                var subjects = await _dataContext.Subject
                  .Select(s => new
                  {
                      s.SubjectId,
                      s.SubjectName,
                      s.PeriodsPerYear,
                      s.Credit
                  })
                  .ToListAsync();

                result.Data = subjects;
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
                var subjectsPaging = new List<object>();
                var subjects = new List<object>();

                if (!string.IsNullOrWhiteSpace(valueFilter))
                {
                    var subjectDb = await _dataContext.Subject
                        .Select(s => new
                        {
                            s.SubjectId,
                            s.SubjectName,
                            s.PeriodsPerYear,
                            s.Credit
                        })
                        .Where(s => s.SubjectName.ToLower().Trim().Contains(valueFilter.ToLower().Trim()))
                        .ToListAsync();

                    subjects = _mapper.Map<List<object>>(subjectDb);

                    subjectsPaging = subjects
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }
                else
                {
                    var subjectDB = await _dataContext.Subject
                        .Select(s => new
                        {
                            s.SubjectId,
                            s.SubjectName,
                            s.PeriodsPerYear,
                            s.Credit
                        })
                        .ToListAsync();

                    subjects = _mapper.Map<List<object>>(subjectDB);

                    subjectsPaging = subjects
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }

                var toTalRecord = subjects.Count;
                var toTalPage = (toTalRecord % pageSize) == 0 ? ((int)toTalRecord / (int)pageSize) : ((int)toTalRecord / (int)pageSize + 1);

                var pagingResult = new PagingResult<object>()
                {
                    ToTalPage = toTalPage,
                    ToTalRecord = toTalRecord,
                    Data = subjectsPaging
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
                var subjectById = await _dataContext.Subject
                    .Select(s => new
                    {
                        s.SubjectId,
                        s.SubjectName,
                        s.PeriodsPerYear,
                        s.Credit
                    })
                    .FirstOrDefaultAsync(s => s.SubjectId == id);

                // Check subjectById is null
                if (subjectById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.SubjectByIdNotFound, id));

                    return result;
                }

                result.Data = subjectById;
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
        /// <param name="subject">Subject</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Insert(Subject subject)
        {
            var result = new OperationResult<bool>();

            try
            {
                var subjectNew = new Subject()
                {
                    SubjectName = subject.SubjectName,
                    PeriodsPerYear = subject.PeriodsPerYear,
                    Credit = subject.Credit,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    CreatedBy = subject.CreatedBy
                };

                _dataContext.Subject.Add(subjectNew);
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
        /// <param name="subject">Subject</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Update(int id, Subject subject)
        {
            var result = new OperationResult<bool>();

            try
            {
                var subjectById = await _dataContext.Subject.FirstOrDefaultAsync(s => s.SubjectId == id);

                // Check subjectById is null
                if (subjectById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.SubjectByIdNotFound, id));

                    return result;
                }

                subjectById.SubjectName = subject.SubjectName;
                subjectById.PeriodsPerYear = subject.PeriodsPerYear;
                subjectById.Credit = subject.Credit;
                subjectById.ModifiedDate = DateTime.UtcNow;
                subjectById.ModifiedBy = subject.ModifiedBy;

                _dataContext.Subject.Update(subjectById);
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