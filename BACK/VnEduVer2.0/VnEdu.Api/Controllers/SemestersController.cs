using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VnEdu.Api.Contracts.Semester.Request;
using VnEdu.Api.Extensions;
using VnEdu.Api.Filters;
using VnEdu.Core.Entities.Models;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;

namespace VnEdu.Api.Controllers
{
    /// <summary>
    /// Information of SemestersController
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [VnEduException]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SemestersController : ControllerBase
    {
        private readonly ISemesterRepository _semesterRepository;
        private readonly ISemesterService _semesterService;

        /// <summary>
        /// SemestersController
        /// </summary>
        /// <param name="semesterRepository">semesterRepository</param>
        /// <param name="semesterService">semesterService</param>
        public SemestersController(ISemesterRepository semesterRepository, ISemesterService semesterService)
        {
            _semesterRepository = semesterRepository;
            _semesterService = semesterService;
        }

        /// <summary>
        /// GetAllSemesters
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpGet]
        public async Task<IActionResult> GetAllSemesters()
        {
            var result = await _semesterRepository.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// GetPagingSemester
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpGet]
        [Route("GetPagingSemester")]
        public async Task<IActionResult> GetPagingSemester(string? valueFilter, [Required] int pageIndex, [Required] int pageSize)
        {
            var result = await _semesterRepository.GetAllPaging(valueFilter, pageIndex, pageSize);

            return Ok(result);
        }

        /// <summary>
        /// GetSemesterById
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSemesterById([Required] int id)
        {
            var result = await _semesterRepository.GetById(id);

            return Ok(result);
        }

        /// <summary>
        /// GetSemesterBySchoolYearId
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(26/03/2023)
        [HttpGet]
        [Route("GetSemesterBySchoolYearId")]
        public async Task<IActionResult> GetSemesterBySchoolYearId([Required] int schoolYearId)
        {
            var result = await _semesterRepository.GetBySchoolYearId(schoolYearId);

            return Ok(result);
        }

        /// <summary>
        /// CreateSemester
        /// </summary>
        /// <param name="semesterCreate">Semester</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpPost]
        public async Task<IActionResult> CreateSemester([FromBody] SemesterCreate semesterCreate)
        {
            var createdBy = HttpContext.GetUserNameClaimValue();

            var semester = new Semester()
            {
                SemesterName = semesterCreate.SemesterName,
                DateStart = semesterCreate.DateStart,
                DateEnd = semesterCreate.DateEnd,
                SchoolYearId = semesterCreate.SchoolYearId,
                CreatedBy = createdBy
            };

            var result = await _semesterService.Insert(semester);

            return Ok(result);
        }

        /// <summary>
        /// UpdateSemester
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="semesterUpdate">Semester</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateSemester(int id, [FromBody] SemesterUpdate semesterUpdate)
        {
            var modifiedBy = HttpContext.GetUserNameClaimValue();

            var semester = new Semester()
            {
                SemesterName = semesterUpdate.SemesterName,
                DateStart = semesterUpdate.DateStart,
                DateEnd = semesterUpdate.DateEnd,
                SchoolYearId = semesterUpdate.SchoolYearId,
                ModifiedBy = modifiedBy
            };

            var result = await _semesterService.Update(id, semester);

            return Ok(result);
        }

        /// <summary>
        /// DeleteSemester
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSemester(int id)
        {
            var result = await _semesterRepository.Delete(id);

            return Ok(result);
        }
    }
}