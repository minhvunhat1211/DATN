using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;

namespace VnEdu.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Information of IDecentralizationRepository
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public interface IDecentralizationRepository : IBaseRepository<Decentralization>
    {
        /// <summary>
        /// GetDecentralizationByName
        /// </summary>
        /// <param name="decentralizationName">DecentralizationName</param>
        /// <returns>Decentralization</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public Task<OperationResult<Decentralization>> GetDecentralizationByName(string decentralizationName);
    }
}