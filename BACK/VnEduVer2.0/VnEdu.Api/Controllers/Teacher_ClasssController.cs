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
    /// Information of Teacher_ClasssController
    /// CreatedBy: MinhVN(03/01/2023)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [VnEduException]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Teacher_ClasssController : ControllerBase
    {
        private readonly ITeacher_ClassRepository _teacher_ClassRepository;
        private readonly ITeacher_ClassService _teacher_ClassService;

        /// <summary>
        /// Teacher_ClasssController
        /// </summary>
        /// <param name="teacher_ClassRepository">teacher_ClassRepository</param>
        /// <param name="teacher_ClassService">teacher_ClassService</param>
        public Teacher_ClasssController(ITeacher_ClassRepository teacher_ClassRepository,
            ITeacher_ClassService teacher_ClassService)
        {
            _teacher_ClassRepository = teacher_ClassRepository;
            _teacher_ClassService = teacher_ClassService;
        }

        /// <summary>
        /// GetByClassSemesterSchooYear
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="classId">ClassId</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(03/01/2023)
        [HttpGet]
        [Route("GetByClassSemesterSchooYear")]
        public async Task<IActionResult> GetByClassSemesterSchooYear(int schoolYearId, int semesterId, int classId)
        {
            var result = await _teacher_ClassRepository.GetAllTeacher_Class(schoolYearId, semesterId, classId);

            return Ok(result);
        }

        /// <summary>
        /// GetPagingTeacherClassByClassSemesterSchoolYear
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="pageIndex">PageIndex</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(03/01/2023)
        [HttpGet]
        [Route("GetPagingTeacherClassByClassSemesterSchoolYear")]
        public async Task<IActionResult> GetPagingTeacherClassByClassSemesterSchoolYear(int schoolYearId, int semesterId,
            int classId, string? valueFilter, [Required] int pageIndex, [Required] int pageSize)
        {
            var result = await _teacher_ClassRepository.GetPagingTeacherClassByClassSemesterSchoolYear(schoolYearId, semesterId,
                classId, valueFilter, pageIndex, pageSize);

            return Ok(result);
        }

        /// <summary>
        /// CreateTeacher_Class
        /// </summary>
        /// <param name="teacherId">TeacherId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(03/01/2023)
        [HttpPost]
        public async Task<IActionResult> CreateTeacher_Class(int teacherId, int classId, int semesterId, int schoolYearId)
        {
            var createdBy = HttpContext.GetUserNameClaimValue();

            var teacher_Class = new Teacher_Class()
            {
                TeacherId = teacherId,
                ClassId = classId,
                SemesterId = semesterId,
                SchoolYearId = schoolYearId,
                CreatedBy = createdBy
            };

            var result = await _teacher_ClassService.Insert(teacher_Class);

            return Ok(result);
        }

        /// <summary>
        /// DeleteTeacher_Class
        /// </summary>
        /// <param name="teacherId">TeacherId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(04/01/2023)
        [HttpDelete]
        [Route("DeleteTeacher_Class")]
        public async Task<IActionResult> DeleteTeacher_Class(int teacherId, int classId, int semesterId, int schoolYearId)
        {
            var result = await _teacher_ClassService.Delete(teacherId, classId, semesterId, schoolYearId);

            return Ok(result);
        }
    }
}