using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VnEdu.Api.Contracts.DetailedTableScore.Request;
using VnEdu.Api.Extensions;
using VnEdu.Api.Filters;
using VnEdu.Core.Entities.Models;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;

namespace VnEdu.Api.Controllers
{
    /// <summary>
    /// Information of DetailedTableScoresController
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [VnEduException]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DetailedTableScoresController : ControllerBase
    {
        private readonly IDetailedTableScoreRepository _detailedTableScoreRepository;
        private readonly IDetailedTableScoreService _detailedTableScoreService;

        /// <summary>
        /// DetailedTableScoresController
        /// </summary>
        /// <param name="detailedTableScoreRepository">detailedTableScoreRepository</param>
        /// <param name="detailedTableScoreService">detailedTableScoreService</param>
        public DetailedTableScoresController(IDetailedTableScoreRepository detailedTableScoreRepository,
            IDetailedTableScoreService detailedTableScoreService)
        {
            _detailedTableScoreRepository = detailedTableScoreRepository;
            _detailedTableScoreService = detailedTableScoreService;
        }

        /// <summary>
        /// GetDetailedTableScoreById
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDetailedTableScoreById(int id)
        {
            var result = await _detailedTableScoreRepository.GetById(id);

            return Ok(result);
        }

        /// <summary>
        /// UpdateDetailedTableScore
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="detailedTableScoreUpdate">DetailedTableScore</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateDetailedTableScore(int id,
            [FromBody] DetailedTableScoreUpdate detailedTableScoreUpdate)
        {
            var modifiedBy = HttpContext.GetUserNameClaimValue();

            var detailedTableScore = new DetailedTableScore()
            {
                FirstOralScore = detailedTableScoreUpdate.FirstOralScore,
                SecondOralScore = detailedTableScoreUpdate.SecondOralScore,
                ThirdOralScore = detailedTableScoreUpdate.ThirdOralScore,
                First15minutesScore = detailedTableScoreUpdate.First15minutesScore,
                Second15minutesScore = detailedTableScoreUpdate.Second15minutesScore,
                Third15minutesScore = detailedTableScoreUpdate.Third15minutesScore,
                OnePeriodScore = detailedTableScoreUpdate.OnePeriodScore,
                FinalScore = detailedTableScoreUpdate.FinalScore,
                ModifiedBy = modifiedBy
            };

            var result = await _detailedTableScoreService.Update(id, detailedTableScore);

            return Ok(result);
        }
    }
}