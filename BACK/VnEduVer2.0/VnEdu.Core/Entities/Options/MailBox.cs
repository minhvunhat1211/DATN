namespace VnEdu.Core.Entities.Options
{
    /// <summary>
    /// Information of MailBox
    /// CreatedBy: MinhVN(13/01/2023)
    /// </summary>
    public class MailBox
    {
        public string ToEmail { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RamdomCode { get; set; } = string.Empty;
        public string MessageBody { get; set; } = string.Empty;
    }
}