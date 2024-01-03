using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VnEdu.Api.Contracts.User.Request;
using VnEdu.Api.Extensions;
using VnEdu.Api.Filters;
using VnEdu.Core.Entities.Models;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;

namespace VnEdu.Api.Controllers
{
    /// <summary>
    /// Information of UsersController
    /// CreatedBy: MinhVN(22/12/2022)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [VnEduException]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        /// <summary>
        /// UsersController
        /// </summary>
        /// <param name="userRepository">userRepository</param>
        /// <param name="userService">userService</param>
        public UsersController(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        /// <summary>
        /// GetAllUsers
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userRepository.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// GetPagingUser
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpGet]
        [Route("GetPagingUser")]
        public async Task<IActionResult> GetPagingUser(string? valueFilter, [Required] int pageIndex, [Required] int pageSize)
        {
            var result = await _userRepository.GetAllPaging(valueFilter, pageIndex, pageSize);

            return Ok(result);
        }

        /// <summary>
        /// GetUserById
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userRepository.GetById(id);

            return Ok(result);
        }

        /// <summary>
        /// GetUserById
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById()
        {
            var userId = HttpContext.GetUserIdClaimValue();

            var result = await _userRepository.GetById(Convert.ToInt32(userId));

            return Ok(result);
        }

        /// <summary>
        /// UpdateUser
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="userUpdate">User</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdate userUpdate)
        {
            var user = new User()
            {
                UserName = userUpdate.UserName,
                PhoneNumber = userUpdate.PhoneNumber,
                Email = userUpdate.Email,
                DecentralizationId = userUpdate.DecentralizationId
            };

            var result = await _userService.Update(id, user);

            return Ok(result);
        }

        /// <summary>
        /// UpdatePassword
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="userUpdatePassword">UserUpdatePassword</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpPut]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] UserUpdatePassword userUpdatePassword)
        {
            var result = await _userService.UpdatePassword(id, userUpdatePassword.Password);

            return Ok(result);
        }

        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.Delete(id);

            return Ok(result);
        }

        /// <summary>
        /// GetUserByIdPassword
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(11/01/2023)
        [HttpPost]
        [Route("GetUserByIdPassword")]
        public async Task<IActionResult> GetUserByIdPassword([FromBody] UserUpdatePassword userUpdatePassword)
        {
            var userId = HttpContext.GetUserIdClaimValue();

            var result = await _userRepository.GetUserByIdPassword(Convert.ToInt32(userId), userUpdatePassword.Password);

            return Ok(result);
        }
    }
}