using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;

namespace VnEdu.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Information of ITeacherRepository
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface ITeacherRepository : IBaseRepository<Teacher>
    {
        /// <summary>
        /// GetTeacherByCode
        /// </summary>
        /// <param name="teacherCode">TeacherCode</param>
        /// <returns>Teacher</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<Teacher>> GetTeacherByCode(string teacherCode);

        /// <summary>
        /// GetTeacherByPhoneNumber
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <returns>Teacher</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<Teacher>> GetTeacherByPhoneNumber(string phoneNumber);

        /// <summary>
        /// GetTeacherByEmail
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Teacher</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<Teacher>> GetTeacherByEmail(string email);

        /// <summary>
        /// GetTeacherByNumberCard
        /// </summary>
        /// <param name="numberCard">NumberCard</param>
        /// <returns>Teacher</returns>
        /// CreatedBy: MinhVN(29/03/2023)
        public Task<OperationResult<Teacher>> GetTeacherByNumberCard(string numberCard);

        /// <summary>
        /// GetInformationTeacherByPhoneNumber
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<object>> GetInformationTeacherByPhoneNumber(string phoneNumber);

        /// <summary>
        /// GetInformationTeacherByEmail
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<object>> GetInformationTeacherByEmail(string email);
    }
}