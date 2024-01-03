using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VnEdu.Api.Contracts.Class.Request;
using VnEdu.Api.Extensions;
using VnEdu.Api.Filters;
using VnEdu.Core.Entities.Models;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;
using VnEdu.Infrastructure.Repositories;

namespace VnEdu.Api.Controllers
{
    /// <summary>
    /// Information of ClasssController
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [VnEduException]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClasssController : ControllerBase
    {
        private readonly IClassRepository _classRepository;
        private readonly IClassService _classService;

        /// <summary>
        /// ClasssController
        /// </summary>
        /// <param name="classRepository">classRepository</param>
        /// <param name="classService">classService</param>
        public ClasssController(IClassRepository classRepository, IClassService classService)
        {
            _classRepository = classRepository;
            _classService = classService;
        }

        /// <summary>
        /// GetAllClasss
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        [HttpGet]
        public async Task<IActionResult> GetAllClasss()
        {
            var result = await _classRepository.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// GetPagingClass
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpGet]
        [Route("GetPagingClass")]
        public async Task<IActionResult> GetPagingClass(string? valueFilter, [Required] int pageIndex, [Required] int pageSize)
        {
            var result = await _classRepository.GetAllPaging(valueFilter, pageIndex, pageSize);

            return Ok(result);
        }

        /// <summary>
        /// GetClassById
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            var result = await _classRepository.GetById(id);

            return Ok(result);
        }

        /// <summary>
        /// GetClassBySchoolYearId
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(26/03/2023)
        [HttpGet]
        [Route("GetClassBySchoolYearId")]
        public async Task<IActionResult> GetClassBySchoolYearId([Required] int schoolYearId)
        {
            var result = await _classRepository.GetBySchoolYearId(schoolYearId);

            return Ok(result);
        }

        /// <summary>
        /// CreateClass
        /// </summary>
        /// <param name="classCreate">Class</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        [HttpPost]
        public async Task<IActionResult> CreateClass([FromBody] ClassCreate classCreate)
        {
            var createdBy = HttpContext.GetUserNameClaimValue();

            var classNew = new Class() 
            { 
                ClassName = classCreate.ClassName,
                TeacherId = classCreate.TeacherId,
                Grade = classCreate.Grade,
                Room = classCreate.Room,
                SchoolYearId = classCreate.SchoolYearId,
                CreatedBy = createdBy,
            };

            var result = await _classService.Insert(classNew);

            return Ok(result);
        }

        /// <summary>
        /// UpdateClass
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="classUpdate">Class</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateClass(int id, [FromBody] ClassUpdate classUpdate)
        {
            var modifiedBy = HttpContext.GetUserNameClaimValue();

            var classOld = new Class() 
            { 
                ClassName = classUpdate.ClassName,
                SchoolYearId = classUpdate.SchoolYearId,
                TeacherId = classUpdate.TeacherId,
                Grade = classUpdate.Grade,
                Room = classUpdate.Room,
                ModifiedBy = modifiedBy
            };

            var result = await _classService.Update(id, classOld);

            return Ok(result);
        }

        /// <summary>
        /// DeleteClass
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var result = await _classService.Delete(id);

            return Ok(result);
        }
    }
}