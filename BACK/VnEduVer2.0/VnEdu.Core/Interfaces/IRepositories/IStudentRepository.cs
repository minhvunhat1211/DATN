using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;

namespace VnEdu.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Information of IStudentRepository
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface IStudentRepository : IBaseRepository<Student>
    {
        /// <summary>
        /// GetStudentByCode
        /// </summary>
        /// <param name="studentCode">StudentCode</param>
        /// <returns>Student</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<Student>> GetStudentByCode(string studentCode);

        /// <summary>
        /// GetStudentByPhoneNumber
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <returns>Student</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<Student>> GetStudentByPhoneNumber(string phoneNumber);

        /// <summary>
        /// GetStudentByEmail
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Student</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<Student>> GetStudentByEmail(string email);

        /// <summary>
        /// GetStudentByNumberCard
        /// </summary>
        /// <param name="numberCard">NumberCard</param>
        /// <returns>Student</returns>
        /// CreatedBy: MinhVN(29/03/2023)
        public Task<OperationResult<Student>> GetStudentByNumberCard(string numberCard);

        /// <summary>
        /// GetInformationStudentByPhoneNumber
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<object>> GetInformationStudentByPhoneNumber(string phoneNumber);

        /// <summary>
        /// GetInformationStudentByEmail
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<object>> GetInformationStudentByEmail(string email);
    }
}