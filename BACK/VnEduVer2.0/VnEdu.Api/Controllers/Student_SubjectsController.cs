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
    /// Information of Student_SubjectsController
    /// CreatedBy: MinhVN(02/01/2023)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [VnEduException]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Student_SubjectsController : ControllerBase
    {
        private readonly IStudent_SubjectRepository _student_SubjectRepository;
        private readonly IStudent_SubjectService _student_SubjectService;

        /// <summary>
        /// Student_SubjectsController
        /// </summary>
        /// <param name="student_SubjectRepository">student_SubjectRepository</param>
        /// <param name="student_SubjectService">student_SubjectService</param>
        public Student_SubjectsController(IStudent_SubjectRepository student_SubjectRepository,
            IStudent_SubjectService student_SubjectService)
        {
            _student_SubjectRepository = student_SubjectRepository;
            _student_SubjectService = student_SubjectService;
        }

        /// <summary>
        /// GetBySubjectSemesterSchooYearClass
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(02/01/2023)
        [HttpGet]
        [Route("GetBySubjectSemesterSchooYearClass")]
        public async Task<IActionResult> GetBySubjectSemesterSchooYearClass(int studentId, int semesterId, int schoolYearId)
        {
            var result = await _student_SubjectRepository.GetAllSubjectStudent_Subject(studentId, semesterId, schoolYearId);

            return Ok(result);
        }

        /// <summary>
        /// GetScoreByStudent
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="studentId">StudentId</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(02/01/2023)
        [HttpGet]
        [Route("GetScoreByStudent")]
        public async Task<IActionResult> GetScoreByStudent(int schoolYearId, int semesterId, int studentId)
        {
            var result = await _student_SubjectRepository.GetScoreByStudent(schoolYearId, semesterId, studentId);

            return Ok(result);
        }

        /// <summary>
        /// GetPagingStudentSubjectBySchoolYearSemesterSubject
        /// </summary>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="pageIndex">PageIndex</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(02/01/2023)
        [HttpGet]
        [Route("GetPagingStudentSubjectBySchoolYearSemesterSubject")]
        public async Task<IActionResult> GetPagingStudentSubjectBySchoolYearSemesterSubject(int schoolYearId, int semesterId,
            int subjectId, int classId, string? valueFilter, [Required] int pageIndex, [Required] int pageSize)
        {
            var result = await _student_SubjectRepository.GetPagingStudentSubjectBySchoolYearSemesterSubjectClass(schoolYearId, semesterId,
                subjectId, classId, valueFilter, pageIndex, pageSize);

            return Ok(result);
        }

        /// <summary>
        /// CreateStudent_Subject
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(02/01/2023)
        [HttpPost]
        public async Task<IActionResult> CreateStudent_Subject(int studentId, int subjectId, int semesterId, int schoolYearId)
        {
            var createdBy = HttpContext.GetUserNameClaimValue();

            var student_Subject = new Student_Subject()
            {
                StudentId = studentId,
                SubjectId = subjectId,
                SemesterId = semesterId,
                SchoolYearId = schoolYearId,
                CreatedBy = createdBy
            };

            var result = await _student_SubjectService.Insert(student_Subject);

            return Ok(result);
        }

        /// <summary>
        /// DeleteStudent_Subject
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(02/01/2023)
        [HttpDelete]
        [Route("DeleteStudent_Subject")]
        public async Task<IActionResult> DeleteStudent_Subject(int studentId, int subjectId, int semesterId, int schoolYearId)
        {
            var result = await _student_SubjectService.Delete(studentId, subjectId, semesterId, schoolYearId);

            return Ok(result);
        }
    }
}