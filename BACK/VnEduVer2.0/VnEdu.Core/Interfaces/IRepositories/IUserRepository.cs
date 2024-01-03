using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;

namespace VnEdu.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Information of IUserRepository
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface IUserRepository : IBaseRepository<User>
    {
        /// <summary>
        /// GetUserByPhoneNumber
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <returns>User</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<User>> GetUserByPhoneNumber(string phoneNumber);

        /// <summary>
        /// GetUserByPhoneNumberPassword
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <param name="password">Password</param>
        /// <returns>string</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<string>> GetUserByPhoneNumberPassword(string phoneNumber, string password);

        /// <summary>
        /// GetUserByEmail
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>User</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<User>> GetUserByEmail(string email);

        /// <summary>
        /// GetUserByEmailPassword
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>string</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<string>> GetUserByEmailPassword(string email, string password);

        /// <summary>
        /// UpdatePassword
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="password">Password</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<bool>> UpdatePassword(int id, string password);

        /// <summary>
        /// GetUserByIdPassword
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="password">Password</param>
        /// <returns>bool</returns>
        /// CreatedBy: MinhVN(11/01/2023)
        public Task<OperationResult<bool>> GetUserByIdPassword(int id, string password);

        /// <summary>
        /// UpdateOTP
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="codeOTP"CodeOTP></param>
        /// <returns>bool</returns>
        /// CreatedBy: MinhVN(13/01/2023)
        public Task<OperationResult<bool>> UpdateOTP(string email, string codeOTP);

        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(15/01/2023)
        public Task<OperationResult<bool>> ChangePassword(string email, string password);
    }
}