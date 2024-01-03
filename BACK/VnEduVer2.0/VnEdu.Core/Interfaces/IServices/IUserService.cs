using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;

namespace VnEdu.Core.Interfaces.IServices
{
    /// <summary>
    /// Information of IClassService
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface IUserService : IBaseService<User>
    {
        /// <summary>
        /// UpdatePassword
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="password">Password</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<bool>> UpdatePassword(int id, string password);

        /// <summary>
        /// UpdateOTP
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>bool</returns>
        /// CreatedBy: MinhVN(13/01/2023)
        public Task<OperationResult<bool>> UpdateOTP(string? email);

        /// <summary>
        /// VerifyOTP
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="codeOTP"CodeOTP></param>
        /// <returns>bool</returns>
        /// CreatedBy: MinhVN(01/13/2023)
        public Task<OperationResult<bool>> VerifyOTP(string email, string codeOTP);

        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<bool>> ChangePassword(string email, string password);
    }
}