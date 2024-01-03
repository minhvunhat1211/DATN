using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VnEdu.Api.Contracts.Teacher.Request;
using VnEdu.Api.Extensions;
using VnEdu.Api.Filters;
using VnEdu.Core.Entities.Models;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;

namespace VnEdu.Api.Controllers
{
    /// <summary>
    /// Information of TeachersController
    /// CreatedBy: MinhVN(24/12/2022)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [VnEduException]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ITeacherService _teacherService;

        /// <summary>
        /// TeachersController
        /// </summary>
        /// <param name="teacherRepository">teacherRepository</param>
        /// <param name="teacherService">teacherService</param>
        public TeachersController(ITeacherRepository teacherRepository, ITeacherService teacherService)
        {
            _teacherRepository = teacherRepository;
            _teacherService = teacherService;
        }

        /// <summary>
        /// GetAllTeachers
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var result = await _teacherRepository.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// GetPagingTeacher
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        [HttpGet]
        [Route("GetPagingTeacher")]
        public async Task<IActionResult> GetPagingTeacher(string? valueFilter, [Required] int pageIndex, [Required] int pageSize)
        {
            var result = await _teacherRepository.GetAllPaging(valueFilter, pageIndex, pageSize);

            return Ok(result);
        }

        /// <summary>
        /// GetTeacherById
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            var result = await _teacherRepository.GetById(id);

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
            var result = await _teacherRepository.GetInformationTeacherByPhoneNumber(phoneNumber);

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
            var result = await _teacherRepository.GetInformationTeacherByEmail(email);

            return Ok(result);
        }

        /// <summary>
        /// CreateTeacher
        /// </summary>
        /// <param name="teacherCreate">TeacherCreate</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] TeacherCreate teacherCreate)
        {
            var createdBy = HttpContext.GetUserNameClaimValue();

            var teacher = new Teacher()
            {
                TeacherCode = teacherCreate.TeacherCode,
                FullName = teacherCreate.FullName,
                Gender = teacherCreate.Gender,
                DateOfBirth = teacherCreate.DateOfBirth,
                PhoneNumber = teacherCreate.PhoneNumber,
                Email = teacherCreate.Email,
                NumberCard = teacherCreate.NumberCard,
                AddressCurent = teacherCreate.AddressCurent,
                Address = teacherCreate.Address,
                CreatedBy = createdBy
            };

            var result = await _teacherService.Insert(teacher);

            return Ok(result);
        }

        /// <summary>
        /// UpdateTeacher
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="teacherUpdate">TeacherUpdate</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] TeacherUpdate teacherUpdate)
        {
            var modifiedBy = HttpContext.GetUserNameClaimValue();

            var teacher = new Teacher()
            {
                TeacherCode = teacherUpdate.TeacherCode,
                FullName = teacherUpdate.FullName,
                Gender = teacherUpdate.Gender,
                DateOfBirth = teacherUpdate.DateOfBirth,
                PhoneNumber = teacherUpdate.PhoneNumber,
                Email = teacherUpdate.Email,
                NumberCard = teacherUpdate.NumberCard,
                AddressCurent = teacherUpdate.AddressCurent,
                Address = teacherUpdate.Address,
                ModifiedBy = modifiedBy
            };

            var result = await _teacherService.Update(id, teacher);

            return Ok(result);
        }

        /// <summary>
        /// DeleteTeacher
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(24/12/2022)
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var result = await _teacherService.Delete(id);

            return Ok(result);
        }
    }
}