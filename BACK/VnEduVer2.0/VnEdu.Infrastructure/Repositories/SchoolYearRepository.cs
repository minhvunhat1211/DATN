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
    /// Information of SchoolYearRepository
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    public class SchoolYearRepository : ISchoolYearRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public SchoolYearRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// GetSchoolYearByName
        /// </summary>
        /// <param name="schoolYearName">SchoolYearName</param>
        /// <returns>SchoolYear</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<SchoolYear>> GetSchoolYearByName(string schoolYearName)
        {
            var result = new OperationResult<SchoolYear>();

            try
            {
                var schoolYearByName = await _dataContext.SchoolYear.FirstOrDefaultAsync(s =>
                    s.SchoolYearName.ToLower().Trim().Equals(schoolYearName.ToLower().Trim()));

                // Check schoolYearByName is null
                if (schoolYearByName is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.SchoolYearByNameNotFound, schoolYearName));

                    return result;
                }

                result.Data = schoolYearByName;
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
                var schoolYearById = await _dataContext.SchoolYear.FirstOrDefaultAsync(s => s.SchoolYearId == id);

                // Check schoolYearById is null
                if (schoolYearById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.SchoolYearByIdNotFound, id));

                    return result;
                }

                _dataContext.SchoolYear.Remove(schoolYearById);
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
                var schoolYears = await _dataContext.SchoolYear
                  .Select( s => new
                  {
                      s.SchoolYearId,
                      s.SchoolYearName,
                      s.DateStart,
                      s.DateEnd
                  })
                  .ToListAsync();

                result.Data = schoolYears;
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
                var schoolYearsPaging = new List<object>();
                var schoolYears = new List<object>();

                if (!string.IsNullOrWhiteSpace(valueFilter))
                {
                    var schoolYearDb = await _dataContext.SchoolYear
                        .Select(s => new
                        {
                            s.SchoolYearId,
                            s.SchoolYearName,
                            s.DateStart,
                            s.DateEnd
                        })
                        .Where(s => s.SchoolYearName.ToLower().Trim().Contains(valueFilter.ToLower().Trim()))
                        .ToListAsync();

                    schoolYears = _mapper.Map<List<object>>(schoolYearDb);

                    schoolYearsPaging = schoolYears
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }
                else
                {
                    var schoolYearDB = await _dataContext.SchoolYear
                        .Select(s => new
                        {
                            s.SchoolYearId,
                            s.SchoolYearName,
                            s.DateStart,
                            s.DateEnd
                        })
                        .ToListAsync();

                    schoolYears = _mapper.Map<List<object>>(schoolYearDB);

                    schoolYearsPaging = schoolYears
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }

                var toTalRecord = schoolYears.Count;
                var toTalPage = (toTalRecord % pageSize) == 0 ? ((int)toTalRecord / (int)pageSize) : ((int)toTalRecord / (int)pageSize + 1);

                var pagingResult = new PagingResult<object>()
                {
                    ToTalPage = toTalPage,
                    ToTalRecord = toTalRecord,
                    Data = schoolYearsPaging
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
                var schoolYearById = await _dataContext.SchoolYear
                    .Select(s => new
                    {
                        s.SchoolYearId,
                        s.SchoolYearName,
                        s.DateStart,
                        s.DateEnd
                    })
                    .FirstOrDefaultAsync(s => s.SchoolYearId == id);

                // Check schoolYearById is null
                if (schoolYearById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.SchoolYearByIdNotFound, id));

                    return result;
                }

                result.Data = schoolYearById;
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
        /// <param name="schoolYear">SchoolYear</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Insert(SchoolYear schoolYear)
        {
            var result = new OperationResult<bool>();

            try
            {
                var schoolYearNew = new SchoolYear()
                {
                    SchoolYearName = schoolYear.SchoolYearName,
                    DateStart = schoolYear.DateStart,
                    DateEnd = schoolYear.DateEnd,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    CreatedBy = schoolYear.CreatedBy
                };

                _dataContext.SchoolYear.Add(schoolYearNew);
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
        /// <param name="schoolYear">SchoolYear</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Update(int id, SchoolYear schoolYear)
        {
            var result = new OperationResult<bool>();

            try
            {
                var schoolYearById = await _dataContext.SchoolYear.FirstOrDefaultAsync(s => s.SchoolYearId == id);

                // Check schoolYearById is null
                if (schoolYearById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.SchoolYearByIdNotFound, id));

                    return result;
                }

                schoolYearById.SchoolYearName = schoolYear.SchoolYearName;
                schoolYearById.DateStart = schoolYear.DateStart;
                schoolYearById.DateEnd = schoolYear.DateEnd;
                schoolYearById.ModifiedDate = DateTime.UtcNow;
                schoolYearById.ModifiedBy = schoolYear.ModifiedBy;

                _dataContext.SchoolYear.Update(schoolYearById);
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