using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VnEdu.Api.Contracts.Subject.Request;
using VnEdu.Api.Extensions;
using VnEdu.Api.Filters;
using VnEdu.Core.Entities.Models;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;

namespace VnEdu.Api.Controllers
{
    /// <summary>
    /// Information of SubjectsController
    /// CreatedBy: MinhVN(23/12/2022)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [VnEduException]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISubjectService _subjectService;

        /// <summary>
        /// SubjectsController
        /// </summary>
        /// <param name="subjectRepository">subjectRepository</param>
        /// <param name="subjectService">subjectService</param>
        public SubjectsController(ISubjectRepository subjectRepository, ISubjectService subjectService)
        {
            _subjectRepository = subjectRepository;
            _subjectService = subjectService;
        }

        /// <summary>
        /// GetAllSubjects
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var result = await _subjectRepository.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// GetPagingSubject
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpGet]
        [Route("GetPagingSubject")]
        public async Task<IActionResult> GetPagingSubject(string? valueFilter, [Required] int pageIndex, [Required] int pageSize)
        {
            var result = await _subjectRepository.GetAllPaging(valueFilter, pageIndex, pageSize);

            return Ok(result);
        }

        /// <summary>
        /// GetSubjectById
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            var result = await _subjectRepository.GetById(id);

            return Ok(result);
        }

        /// <summary>
        /// CreateSubject
        /// </summary>
        /// <param name="subjectCreate">Subject</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectCreate subjectCreate)
        {
            var createdBy = HttpContext.GetUserNameClaimValue();

            var subject = new Subject()
            {
                SubjectName = subjectCreate.SubjectName,
                PeriodsPerYear = subjectCreate.PeriodsPerYear,
                Credit = subjectCreate.Credit,
                CreatedBy = createdBy
            };

            var result = await _subjectService.Insert(subject);

            return Ok(result);
        }

        /// <summary>
        /// UpdateSubject
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="subjectUpdate">Subject</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, [FromBody] SubjectUpdate subjectUpdate)
        {
            var modifiedBy = HttpContext.GetUserNameClaimValue();

            var subject = new Subject()
            {
                SubjectName = subjectUpdate.SubjectName,
                PeriodsPerYear = subjectUpdate.PeriodsPerYear,
                Credit = subjectUpdate.Credit,
                ModifiedBy = modifiedBy
            };

            var result = await _subjectService.Update(id, subject);

            return Ok(result);
        }

        /// <summary>
        /// DeleteSubject
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var result = await _subjectRepository.Delete(id);

            return Ok(result);
        }
    }
}