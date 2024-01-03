namespace VnEdu.Api.Contracts.DetailedTableScore.Request
{
    /// <summary>
    /// Information of DetailedTableScoreUpdate
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    public class DetailedTableScoreUpdate
    {
        /// <summary>
        /// FirstOralScore
        /// </summary>
        public double? FirstOralScore { get; set; }

        /// <summary>
        /// SecondOralScore
        /// </summary>
        public double? SecondOralScore { get; set; }

        /// <summary>
        /// ThirdOralScore
        /// </summary>
        public double? ThirdOralScore { get; set; }

        /// <summary>
        /// First15minutesScore
        /// </summary>
        public double? First15minutesScore { get; set; }

        /// <summary>
        /// Second15minutesScore
        /// </summary>
        public double? Second15minutesScore { get; set; }

        /// <summary>
        /// Third15minutesScore
        /// </summary>
        public double? Third15minutesScore { get; set; }

        /// <summary>
        /// OnePeriodScore
        /// </summary>
        public double? OnePeriodScore { get; set; }

        /// <summary>
        /// FinalScore
        /// </summary>
        public double? FinalScore { get; set; }
    }
}
