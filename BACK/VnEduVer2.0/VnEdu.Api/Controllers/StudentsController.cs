using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VnEdu.Api.Contracts.Student.Request;
using VnEdu.Api.Extensions;
using VnEdu.Api.Filters;
using VnEdu.Core.Entities.Models;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;

namespace VnEdu.Api.Controllers
{
    /// <summary>
    /// Information of StudentsController
    /// CreatedBy: MinhVN(24/12/2022)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [VnEduException]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentService _studentService;

        /// <summary>
        /// StudentsController
        /// </summary>
        /// <param name="studentRepository">studentRepository</param>
        /// <param name="studentService">studentService</param>
        public StudentsController(IStudentRepository studentRepository, IStudentService studentService)
        {
            _studentRepository = studentRepository;
            _studentService = studentService;
        }

        /// <summary>
        /// GetAllStudents
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _studentRepository.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// GetPagingStudent
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        [HttpGet]
        [Route("GetPagingStudent")]
        public async Task<IActionResult> GetPagingStudent(string? valueFilter, [Required] int pageIndex, [Required] int pageSize)
        {
            var result = await _studentRepository.GetAllPaging(valueFilter, pageIndex, pageSize);

            return Ok(result);
        }

        /// <summary>
        /// GetStudentById
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var result = await _studentRepository.GetById(id);

            return Ok(result);
        }

        /// <summary>
        /// GetInformationByPhonenumber
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        [HttpGet]
        [Route("GetInformationByPhonenumber")]
        public async Task<IActionResult> GetInformationByPhonenumber(string phoneNumber)
        {
            var result = await _studentRepository.GetInformationStudentByPhoneNumber(phoneNumber);

            return Ok(result);
        }

        /// <summary>
        /// GetInformationByEmail
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        [HttpGet]
        [Route("GetInformationByEmail")]
        public async Task<IActionResult> GetInformationByEmail(string email)
        {
            var result = await _studentRepository.GetInformationStudentByEmail(email);

            return Ok(result);
        }

        /// <summary>
        /// CreateStudent
        /// </summary>
        /// <param name="studentCreate">StudentCreate</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentCreate studentCreate)
        {
            var createdBy = HttpContext.GetUserNameClaimValue();

            var student = new Student()
            {
                StudentCode = studentCreate.StudentCode,
                FullName = studentCreate.FullName,
                Gender = studentCreate.Gender,
                DateOfBirth = studentCreate.DateOfBirth,
                PhoneNumber = studentCreate.PhoneNumber,
                Email = studentCreate.Email,
                NumberCard = studentCreate.NumberCard,
                AddressCurent = studentCreate.AddressCurent,
                Address = studentCreate.Address,
                CreatedBy = createdBy
            };

            var result = await _studentService.Insert(student);

            return Ok(result);
        }

        /// <summary>
        /// UpdateStudent
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="studentUpdate">StudentUpdate</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentUpdate studentUpdate)
        {
            var modifiedBy = HttpContext.GetUserNameClaimValue();

            var student = new Student()
            {
                StudentCode = studentUpdate.StudentCode,
                FullName = studentUpdate.FullName,
                Gender = studentUpdate.Gender,
                DateOfBirth = studentUpdate.DateOfBirth,
                PhoneNumber = studentUpdate.PhoneNumber,
                Email = studentUpdate.Email,
                NumberCard = studentUpdate.NumberCard,
                AddressCurent = studentUpdate.AddressCurent,
                Address = studentUpdate.Address,
                ModifiedBy = modifiedBy
            };

            var result = await _studentService.Update(id, student);

            return Ok(result);
        }

        /// <summary>
        /// DeleteStudent
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await _studentService.Delete(id);

            return Ok(result);
        }
        [HttpPost]
        [Route("QnA")]
        [AllowAnonymous]

        public async Task<IActionResult> Chat(string question)
        {
            var result = await _studentService.Chat(question);

            return Ok(result);
        }
    }
}