using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VnEdu.Api.Contracts.SchoolYear.Request;
using VnEdu.Api.Extensions;
using VnEdu.Api.Filters;
using VnEdu.Core.Entities.Models;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;

namespace VnEdu.Api.Controllers
{
    /// <summary>
    /// Information of SchoolYearsController
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [VnEduException]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SchoolYearsController : ControllerBase
    {
        private readonly ISchoolYearRepository _schoolYearRepository;
        private readonly ISchoolYearService _schoolYearService;

        /// <summary>
        /// SchoolYearsController
        /// </summary>
        /// <param name="schoolYearRepository">schoolYearRepository</param>
        /// <param name="schoolYearService">schoolYearService</param>
        public SchoolYearsController(ISchoolYearRepository schoolYearRepository, ISchoolYearService schoolYearService)
        {
            _schoolYearRepository = schoolYearRepository;
            _schoolYearService = schoolYearService;
        }

        /// <summary>
        /// GetAllSchoolYears
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpGet]
        public async Task<IActionResult> GetAllSchoolYears()
        {
            var result = await _schoolYearRepository.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// GetPagingSchoolYear
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpGet]
        [Route("GetPagingSchoolYear")]
        public async Task<IActionResult> GetPagingSchoolYear(string? valueFilter, [Required] int pageIndex, [Required] int pageSize)
        {
            var result = await _schoolYearRepository.GetAllPaging(valueFilter, pageIndex, pageSize);

            return Ok(result);
        }

        /// <summary>
        /// GetSchoolYearById
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSchoolYearById(int id)
        {
            var result = await _schoolYearRepository.GetById(id);

            return Ok(result);
        }

        /// <summary>
        /// CreateSchoolYear
        /// </summary>
        /// <param name="schoolYearCreate">SchoolYear</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpPost]
        public async Task<IActionResult> CreateSchoolYear([FromBody] SchoolYearCreate schoolYearCreate)
        {
            var createdBy = HttpContext.GetUserNameClaimValue();

            var schoolYear = new SchoolYear()
            {
                SchoolYearName = schoolYearCreate.SchoolYearName,
                DateStart = schoolYearCreate.DateStart,
                DateEnd = schoolYearCreate.DateEnd,
                CreatedBy = createdBy
            };

            var result = await _schoolYearService.Insert(schoolYear);

            return Ok(result);
        }

        /// <summary>
        /// UpdateSchoolYear
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="schoolYearUpdate">SchoolYear</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateSchoolYear(int id, [FromBody] SchoolYearUpdate schoolYearUpdate)
        {
            var modifiedBy = HttpContext.GetUserNameClaimValue();

            var schoolYear = new SchoolYear()
            {
                SchoolYearName = schoolYearUpdate.SchoolYearName,
                DateStart = schoolYearUpdate.DateStart,
                DateEnd = schoolYearUpdate.DateEnd,
                ModifiedBy = modifiedBy
            };

            var result = await _schoolYearService.Update(id, schoolYear);

            return Ok(result);
        }

        /// <summary>
        /// DeleteSchoolYear
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSchoolYear(int id)
        {
            var result = await _schoolYearRepository.Delete(id);

            return Ok(result);
        }
    }
}