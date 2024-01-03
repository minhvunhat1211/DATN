using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VnEdu.Api.Extensions;
using VnEdu.Api.Filters;
using VnEdu.Core.Entities.Models;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;

namespace VnEdu.Api.Controllers
{
    /// <summary>
    /// Information of Teacher_SubjectsController
    /// CreatedBy: MinhVN(04/01/2023)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [VnEduException]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Teacher_SubjectsController : ControllerBase
    {
        private readonly ITeacher_SubjectRepository _teacher_SubjectRepository;
        private readonly ITeacher_SubjectService _teacher_SubjectService;

        /// <summary>
        /// Teacher_SubjectsController
        /// </summary>
        /// <param name="teacher_SubjectRepository">teacher_SubjectRepository</param>
        /// <param name="teacher_SubjectService">teacher_SubjectService</param>
        public Teacher_SubjectsController(ITeacher_SubjectRepository teacher_SubjectRepository,
            ITeacher_SubjectService teacher_SubjectService)
        {
            _teacher_SubjectRepository = teacher_SubjectRepository;
            _teacher_SubjectService = teacher_SubjectService;
        }

        /// <summary>
        /// GetBySubjectSemesterSchooYear
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(04/01/2023)
        [HttpGet]
        [Route("GetBySubjectSemesterSchooYear")]
        public async Task<IActionResult> GetBySubjectSemesterSchooYear(int subjectId, int semesterId, int schoolYearId)
        {
            var result = await _teacher_SubjectRepository.GetAllTeacher_Subject(subjectId, semesterId, schoolYearId);

            return Ok(result);
        }

        /// <summary>
        /// GetPagingTeacherSubjectByClassSemesterSchoolYear
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="pageIndex">PageIndex</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(04/01/2023)
        [HttpGet]
        [Route("GetPagingTeacherSubjectByClassSemesterSchoolYear")]
        public async Task<IActionResult> GetPagingTeacherSubjectByClassSemesterSchoolYear(int schoolYearId, int semesterId,
            int subjectId, string? valueFilter, [Required] int pageIndex, [Required] int pageSize)
        {
            var result = await _teacher_SubjectRepository.GetPagingTeacherSubjectByClassSemesterSchoolYear(schoolYearId, semesterId,
                subjectId, valueFilter, pageIndex, pageSize);

            return Ok(result);
        }

        /// <summary>
        /// CreateTeacher_Subject
        /// </summary>
        /// <param name="teacherId">TeacherId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(04/01/2023)
        [HttpPost]
        public async Task<IActionResult> CreateTeacher_Subject(int teacherId, int subjectId, int semesterId, int schoolYearId)
        {
            var createdBy = HttpContext.GetUserNameClaimValue();

            var teacher_Subject = new Teacher_Subject()
            {
                TeacherId = teacherId,
                SubjectId = subjectId,
                SemesterId = semesterId,
                SchoolYearId = schoolYearId,
                CreatedBy = createdBy
            };

            var result = await _teacher_SubjectService.Insert(teacher_Subject);

            return Ok(result);
        }

        /// <summary>
        /// DeleteTeacher_Subject
        /// </summary>
        /// <param name="teacherId">TeacherId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(04/01/2023)
        [HttpDelete]
        [Route("DeleteTeacher_Subject")]
        public async Task<IActionResult> DeleteTeacher_Subject(int teacherId, int subjectId, int semesterId, int schoolYearId)
        {
            var result = await _teacher_SubjectService.Delete(teacherId, subjectId, semesterId, schoolYearId);

            return Ok(result);
        }
    }
}