using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VnEdu.Api.Contracts.Decentralizaion.Request;
using VnEdu.Api.Filters;
using VnEdu.Core.Entities.Models;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;

namespace VnEdu.Api.Controllers
{
    /// <summary>
    /// Information of DecentralizationsController
    /// CreatedBy: MinhVN(22/12/2022)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [VnEduException]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DecentralizationsController : ControllerBase
    {
        private readonly IDecentralizationRepository _decentralizationRepository;
        private readonly IDecentralizationService _decentralizationService;

        /// <summary>
        /// DecentralizationsController
        /// </summary>
        /// <param name="decentralizationRepository">decentralizationRepository</param>
        /// <param name="decentralizationService">decentralizationService</param>
        public DecentralizationsController(IDecentralizationRepository decentralizationRepository,
            IDecentralizationService decentralizationService)
        {
            _decentralizationRepository = decentralizationRepository;
            _decentralizationService = decentralizationService;
        }

        /// <summary>
        /// GetAllDecentralizaions
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpGet]
        public async Task<IActionResult> GetAllDecentralizaions()
        {
            var result = await _decentralizationRepository.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// GetPagingDecentralization
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpGet]
        [Route("GetPagingDecentralization")]
        public async Task<IActionResult> GetPagingDecentralization(string? valueFilter, [Required] int pageIndex, [Required] int pageSize)
        {
            var result = await _decentralizationRepository.GetAllPaging(valueFilter, pageIndex, pageSize);

            return Ok(result);
        }

        /// <summary>
        /// GetDecentralizaionById
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDecentralizaionById(int id)
        {
            var result = await _decentralizationRepository.GetById(id);

            return Ok(result);
        }

        /// <summary>
        /// CreateDecentralizaion
        /// </summary>
        /// <param name="decentralizaionCreate">Decentralization</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpPost]
        public async Task<IActionResult> CreateDecentralizaion([FromBody] DecentralizaionCreate decentralizaionCreate)
        {
            var decentralization = new Decentralization()
            {
                DecentralizationName = decentralizaionCreate.DecentralizationName,
                Description = decentralizaionCreate.Description
            };

            var result = await _decentralizationService.Insert(decentralization);

            return Ok(result);
        }

        /// <summary>
        /// UpdateDecentralization
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="decentralizaionUpdate">Decentralization</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateDecentralization(int id, [FromBody] DecentralizaionUpdate decentralizaionUpdate)
        {
            var decentralization = new Decentralization()
            {
                DecentralizationName = decentralizaionUpdate.DecentralizationName,
                Description = decentralizaionUpdate.Description
            };

            var result = await _decentralizationService.Update(id, decentralization);

            return Ok(result);
        }

        /// <summary>
        /// DeleteDecentralization
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDecentralization(int id)
        {
            var result = await _decentralizationService.Delete(id);

            return Ok(result);
        }
    }
}