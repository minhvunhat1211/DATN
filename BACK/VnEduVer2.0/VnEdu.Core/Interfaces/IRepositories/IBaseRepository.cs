using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Options;

namespace VnEdu.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Information of IBaseRepository
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns>List object</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<IEnumerable<object>>> GetAll();

        /// <summary>
        /// GetAllPaging
        /// </summary>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="pageIndex">PageIndex</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>PagingResult</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<PagingResult<object>>> GetAllPaging(string? valueFilter, int pageIndex, int pageSize);

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<object>> GetById(int id);

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="t">T</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<bool>> Insert(T t);

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="t"><T/param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<bool>> Update(int id, T t);

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<bool>> Delete(int id);
    }
}