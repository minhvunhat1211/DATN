namespace VnEdu.Core.Entities.Results
{
    /// <summary>
    /// Information of Error
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Code
        /// </summary>
        public ErrorCode Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}