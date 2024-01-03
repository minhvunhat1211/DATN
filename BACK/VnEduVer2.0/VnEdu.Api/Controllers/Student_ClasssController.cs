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
    /// Information of Student_ClasssController
    /// CreatedBy: MinhVN(25/12/2022)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [VnEduException]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Student_ClasssController : ControllerBase
    {
        private readonly IStudent_ClassRepository _student_ClassRepository;
        private readonly IStudent_ClassService _student_ClassService;

        /// <summary>
        /// Student_ClasssController
        /// </summary>
        /// <param name="student_ClassRepository">student_ClassRepository</param>
        /// <param name="student_ClassService">student_ClassService</param>
        public Student_ClasssController(IStudent_ClassRepository student_ClassRepository,
            IStudent_ClassService student_ClassService)
        {
            _student_ClassRepository = student_ClassRepository;
            _student_ClassService = student_ClassService;
        }

        /// <summary>
        /// GetByClassSemesterSchooYear
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="classId">ClassId</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(25/12/2022)
        [HttpGet]
        [Route("GetByClassSemesterSchooYear")]
        public async Task<IActionResult> GetByClassSemesterSchooYear(int schoolYearId, int semesterId, int classId)
        {
            var result = await _student_ClassRepository.GetAllStudentStudent_Class(schoolYearId, semesterId, classId);

            return Ok(result);
        }

        /// <summary>
        /// GetPagingStudentClassByClassSemesterSchooYear
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="pageIndex">PageIndex</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(25/12/2022)
        [HttpGet]
        [Route("GetPagingStudentClassByClassSemesterSchooYear")]
        public async Task<IActionResult> GetPagingStudentClassByClassSemesterSchooYear(int schoolYearId, int semesterId,
            int classId, string? valueFilter, [Required] int pageIndex, [Required] int pageSize)
        {
            var result = await _student_ClassRepository.GetPagingStudentClassByClassSemesterSchoolYear(schoolYearId, semesterId,
                classId, valueFilter, pageIndex, pageSize);

            return Ok(result);
        }

        /// <summary>
        /// GetClassByStudentSchooYearSemester
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(25/12/2022)
        [HttpGet]
        [Route("GetClassByStudentSchooYearSemester")]
        public async Task<IActionResult> GetClassByStudentSchooYearSemester(int studentId, int schoolYearId, int semesterId)
        {
            var result = await _student_ClassRepository.GetClassByStudentSchoolYearSemester(studentId, schoolYearId, semesterId);

            return Ok(result);
        }

        /// <summary>
        /// CreateStudent_Class
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(25/12/2022)
        [HttpPost]
        public async Task<IActionResult> CreateStudent_Class(int studentId, int classId, int semesterId, int schoolYearId)
        {
            var createdBy = HttpContext.GetUserNameClaimValue();

            var student_Class = new Student_Class()
            {
                StudentId = studentId,
                ClassId = classId,
                SemesterId = semesterId,
                SchoolYearId = schoolYearId,
                CreatedBy = createdBy
            };

            var result = await _student_ClassService.Insert(student_Class);

            return Ok(result);
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(25/12/2022)
        [HttpDelete]
        [Route("DeleteStudent_Class")]
        public async Task<IActionResult> DeleteStudent_Class(int studentId, int classId, int semesterId, int schoolYearId)
        {
            var result = await _student_ClassService.Delete(studentId, classId, semesterId, schoolYearId);

            return Ok(result);
        }
    }
}