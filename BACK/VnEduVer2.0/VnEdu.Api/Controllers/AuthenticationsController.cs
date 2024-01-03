using Microsoft.AspNetCore.Mvc;
using VnEdu.Api.Contracts.User.Request;
using VnEdu.Api.Filters;
using VnEdu.Core.Entities.Models;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;

namespace VnEdu.Api.Controllers
{
    /// <summary>
    /// Information of AuthenticationsController
    /// CreatedBy: MinhVN(26/12/2022)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [VnEduException]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        /// <summary>
        /// AuthenticationsController
        /// </summary>
        /// <param name="userRepository">userRepository</param>
        /// <param name="userService">userService</param>
        public AuthenticationsController(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        /// <summary>
        /// LoginForPhoneNumber
        /// </summary>
        /// <param name="userPhoneNumberPassword">UserPhoneNumberPassword</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(26/12/2022)
        [HttpPost]
        [Route("LoginForPhoneNumber")]
        public async Task<IActionResult> LoginForPhoneNumber([FromBody] UserPhoneNumberPassword userPhoneNumberPassword)
        {
            var result = await _userRepository
                .GetUserByPhoneNumberPassword(userPhoneNumberPassword.PhoneNumber, userPhoneNumberPassword.Password);

            return Ok(result);
        }

        /// <summary>
        /// LoginForEmail
        /// </summary>
        /// <param name="userEmailPassword">UserEmailPassword</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(26/12/2022)
        [HttpPost]
        [Route("LoginForEmail")]
        public async Task<IActionResult> LoginForEmail([FromBody] UserEmailPassword userEmailPassword)
        {
            var result = await _userRepository
                .GetUserByEmailPassword(userEmailPassword.Email, userEmailPassword.Password);

            return Ok(result);
        }

        /// <summary>
        /// CreateUser
        /// </summary>
        /// <param name="userCreate">User</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(26/12/2022)
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreate userCreate)
        {
            var user = new User()
            {
                UserName = userCreate.UserName,
                PhoneNumber = userCreate.PhoneNumber,
                Email = userCreate.Email,
                DecentralizationId = userCreate.DecentralizationId
            };

            var result = await _userService.Insert(user);

            return Ok(result);
        }

        /// <summary>
        /// SendOTPByEmail
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(13/01/2023)
        [HttpPost]
        [Route("SendOTPByEmail")]
        public async Task<IActionResult> SendOTPByEmail(string? email)
        {
            var result = await _userService.UpdateOTP(email);

            return Ok(result);
        }

        /// <summary>
        /// VerifyOTP
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="codeOTP">CodeOTP</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(13/01/2023)
        [HttpPost]
        [Route("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP(string email, string codeOTP)
        {
            var result = await _userService.VerifyOTP(email, codeOTP);

            return Ok(result);
        }

        /// <summary>
        /// ChangePasswordNew
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="userUpdatePassword">UserUpdatePassword</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: MinhVN(15/01/2023)
        [HttpPut]
        [Route("ChangePasswordNew")]
        public async Task<IActionResult> ChangePasswordNew(string email, [FromBody] UserUpdatePassword userUpdatePassword)
        {
            var result = await _userService.ChangePassword(email, userUpdatePassword.Password);

            return Ok(result);
        }
    }
}