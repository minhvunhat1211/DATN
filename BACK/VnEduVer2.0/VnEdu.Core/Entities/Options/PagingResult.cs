namespace VnEdu.Core.Entities.Options
{
    /// <summary>
    /// Information of PagingResult
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    /// CreatedBy: MinhVN(21/12/2022)
    public class PagingResult<T>
    {
        /// <summary>
        /// ToTalRecord
        /// </summary>
        public int ToTalRecord { get; set; }

        /// <summary>
        /// ToTalPage
        /// </summary>
        public int ToTalPage { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
    }
}