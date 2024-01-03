using VnEdu.Core.Entities.OperationResult;

namespace VnEdu.Core.Interfaces.IServices
{
    /// <summary>
    /// Information of IBaseService
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public interface IBaseService<T>
    {
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
        /// <param name="t">T</param>
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