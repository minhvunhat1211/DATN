namespace VnEdu.Api.Contracts.Decentralizaion.Request
{
    /// <summary>
    /// Information of DecentralizaionCreate
    /// CreatedBy: MinhVN(22/12/2022)
    /// </summary>
    public class DecentralizaionCreate
    {
        /// <summary>
        /// DecentralizationName
        /// </summary>
        public string DecentralizationName { get; set; } = string.Empty;

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
