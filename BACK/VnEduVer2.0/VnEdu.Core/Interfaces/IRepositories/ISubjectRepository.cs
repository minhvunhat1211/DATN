using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;

namespace VnEdu.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Information of ISubjectRepository
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface ISubjectRepository : IBaseRepository<Subject>
    {
        /// <summary>
        /// GetSubjectByName
        /// </summary>
        /// <param name="subjectName">SubjectName</param>
        /// <returns>Subject</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<Subject>> GetSubjectByName(string subjectName);
    }
}