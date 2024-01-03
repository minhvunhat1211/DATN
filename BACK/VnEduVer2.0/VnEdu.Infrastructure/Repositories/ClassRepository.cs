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
    /// Information of ClassRepository
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public class ClassRepository : IClassRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ClassRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record Delete success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public async Task<OperationResult<bool>> Delete(int id)
        {
            var result = new OperationResult<bool>();

            try
            {
                var classById = await _dataContext.Class.FirstOrDefaultAsync(c => c.ClassId == id);

                // Check classById is null
                if (classById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.ClassByIdNotFound, id));

                    return result;
                }

                _dataContext.Class.Remove(classById);
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
        /// CreatedBy: MinhVN(21/12/2022)
        public async Task<OperationResult<IEnumerable<object>>> GetAll()
        {
            var result = new OperationResult<IEnumerable<object>>();

            try
            {
                var classs = await (from c in _dataContext.Class
                    join sy in _dataContext.SchoolYear on c.SchoolYearId equals sy.SchoolYearId
                    join t in _dataContext.Teacher on c.TeacherId equals t.TeacherId
                    select new
                    {
                        c.ClassId,
                        c.ClassName,
                        c.TeacherId,
                        t.FullName,
                        c.Grade,
                        c.Room,
                        c.SchoolYearId,
                        sy.SchoolYearName
                    })
                    .ToListAsync();

                result.Data = _mapper.Map<IEnumerable<object>>(classs);
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
        /// CreatedBy: MinhVN(22/12/2022)
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
                var classsPaging = new List<object>();
                var classs = new List<object>();

                if (!string.IsNullOrWhiteSpace(valueFilter))
                {
                    var calsssDb = await (from c in _dataContext.Class
                        join sy in _dataContext.SchoolYear on c.SchoolYearId equals sy.SchoolYearId
                        join t in _dataContext.Teacher on c.TeacherId equals t.TeacherId
                        select new
                        {
                            c.ClassId,
                            c.ClassName,
                            c.TeacherId,
                            t.FullName,
                            c.Grade,
                            c.Room,
                            c.SchoolYearId,
                            sy.SchoolYearName
                        })
                        .Where(d => d.ClassName.ToLower().Trim().Contains(valueFilter.ToLower().Trim()))
                        .ToListAsync();

                    classs = _mapper.Map<List<object>>(calsssDb);

                    classsPaging = classs
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }
                else
                {
                    var classDB = await (from c in _dataContext.Class
                        join sy in _dataContext.SchoolYear on c.SchoolYearId equals sy.SchoolYearId
                        join t in _dataContext.Teacher on c.TeacherId equals t.TeacherId
                        select new
                        {
                            c.ClassId,
                            c.ClassName,
                            c.TeacherId,
                            t.FullName,
                            c.Grade,
                            c.Room,
                            c.SchoolYearId,
                            sy.SchoolYearName
                        })
                        .ToListAsync();

                    classs = _mapper.Map<List<object>>(classDB);

                    classsPaging = classs
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }

                var toTalRecord = classs.Count;
                var toTalPage = (toTalRecord % pageSize) == 0 ? ((int)toTalRecord / (int)pageSize) : ((int)toTalRecord / (int)pageSize + 1);

                var pagingResult = new PagingResult<object>()
                {
                    ToTalPage = toTalPage,
                    ToTalRecord = toTalRecord,
                    Data = classsPaging
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
        /// CreatedBy: MinhVN(21/12/2022)
        public async Task<OperationResult<object>> GetById(int id)
        {
            var result = new OperationResult<object>();

            try
            {
                var classById = await (from c in _dataContext.Class
                    join sy in _dataContext.SchoolYear on c.SchoolYearId equals sy.SchoolYearId
                    join t in _dataContext.Teacher on c.TeacherId equals t.TeacherId
                    select new
                    {
                        c.ClassId,
                        c.ClassName,
                        c.TeacherId,
                        t.FullName,
                        c.Grade,
                        c.Room,
                        c.SchoolYearId,
                        sy.SchoolYearName
                    })
                    .FirstOrDefaultAsync(c => c.ClassId == id);

                // Check classById is null
                if (classById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.ClassByIdNotFound, id));

                    return result;
                }

                result.Data = _mapper.Map<object>(classById);
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
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>List Class</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public async Task<OperationResult<IEnumerable<object>>> GetBySchoolYearId(int schoolYearId)
        {
            var result = new OperationResult<IEnumerable<object>>();

            try
            {
                var classsBySchoolYearId = await (from c in _dataContext.Class
                    join sy in _dataContext.SchoolYear on c.SchoolYearId equals sy.SchoolYearId
                    join t in _dataContext.Teacher on c.TeacherId equals t.TeacherId
                    select new
                    {
                        c.ClassId,
                        c.ClassName,
                        c.TeacherId,
                        t.FullName,
                        c.Grade,
                        c.Room,
                        c.SchoolYearId,
                        sy.SchoolYearName
                    })
                    .Where(c => c.SchoolYearId == schoolYearId)
                    .ToListAsync();

                result.Data = classsBySchoolYearId;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetBySchoolYearTeacher
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="teacherId">TeacherId</param>
        /// <returns>Class</returns>
        /// CreatedBy: MinhVN(25/03/2022)
        public async Task<OperationResult<Class>> GetBySchoolYearTeacher(int schoolYearId, int teacherId)
        {
            var result = new OperationResult<Class>();

            try
            {
                var classBySchoolYearTeacher = await _dataContext.Class.FirstOrDefaultAsync(c =>
                    c.SchoolYearId == schoolYearId && c.TeacherId == teacherId);

                // Check classBySchoolYearTeacher is null
                if (classBySchoolYearTeacher is null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.ClassBySchoolYearTeacherNotFound);

                    return result;
                }

                result.Data = classBySchoolYearTeacher;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetClassByName
        /// </summary>
        /// <param name="className">ClassName</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>Class</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public async Task<OperationResult<Class>> GetClassByName(string className, int schoolYearId)
        {
            var result = new OperationResult<Class>();

            try
            {
                var classByName = await _dataContext.Class.FirstOrDefaultAsync(c =>
                    c.ClassName.ToLower().Trim().Equals(className.ToLower().Trim())
                    && c.SchoolYearId == schoolYearId);

                // Check classByName is null
                if (classByName is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.ClassByNameNotFound, className));

                    return result;
                }

                result.Data = classByName;
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
        /// <param name="classCreate">Class</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public async Task<OperationResult<bool>> Insert(Class classCreate)
        {
            var result = new OperationResult<bool>();

            try
            {
                var classNew = new Class()
                {
                    ClassName = classCreate.ClassName,
                    TeacherId = classCreate.TeacherId,
                    Grade = classCreate.Grade,
                    Room = classCreate.Room,
                    SchoolYearId = classCreate.SchoolYearId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    CreatedBy = classCreate.CreatedBy
                };

                _dataContext.Class.Add(classNew);
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
        /// <param name="classUpdate">Class</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public async Task<OperationResult<bool>> Update(int id, Class classUpdate)
        {
            var result = new OperationResult<bool>();

            try
            {
                var classById = await _dataContext.Class.FirstOrDefaultAsync(c => c.ClassId == id);

                // Check classById is null
                if (classById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.ClassByIdNotFound, id));

                    return result;
                }

                classById.ClassName = classUpdate.ClassName;
                classById.TeacherId = classUpdate.TeacherId;
                classById.Grade = classUpdate.Grade;
                classById.Room = classUpdate.Room;
                classById.SchoolYearId = classUpdate.SchoolYearId;
                classById.ModifiedDate = DateTime.UtcNow;
                classById.ModifiedBy = classUpdate.ModifiedBy;

                _dataContext.Class.Update(classById);
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