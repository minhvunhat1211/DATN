using System.Net.Mail;
using System.Net;
using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Options;
using VnEdu.Core.Entities.Results;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;
using VnEdu.Core.Services.Commom;

namespace VnEdu.Core.Services
{
    /// <summary>
    /// Information of UserService
    /// CreatedBy: MinhVN(22/12/2022)
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDecentralizationRepository _decentralizationRepository;

        public UserService(IUserRepository userRepository, IDecentralizationRepository decentralizationRepository)
        {
            _userRepository = userRepository;
            _decentralizationRepository = decentralizationRepository;
        }

        /// <summary>
        /// UpdateOTP
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>bool</returns>
        /// CreatedBy: MinhVN(13/01/2023)
        public async Task<OperationResult<bool>> UpdateOTP(string? email)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. Email is null
            if (string.IsNullOrWhiteSpace(email))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailNotEmpty);

                return result;
            }

            // Get userByEmail
            var userByEmail = await _userRepository.GetUserByEmail(email);

            // 2. userByEmail is null
            if (userByEmail.Data is null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailNotFound);

                return result;
            }

            try
            {
                var mailBox = new MailBox()
                {
                    FromEmail = ConfigSystem.FromEmail,
                    ToEmail = email.ToString().Trim(),
                    Password = ConfigSystem.Password,
                    RamdomCode = RandomCodeOTP(),

                };
                mailBox.MessageBody = string.Format(ConfigSystem.MessageBody, mailBox.RamdomCode);

                // MailMessage
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(mailBox.ToEmail);
                mailMessage.From = new MailAddress(mailBox.FromEmail);
                mailMessage.Body = mailBox.MessageBody;
                mailMessage.Subject = ConfigSystem.OTPVerity;

                // SmtpClient
                SmtpClient smtpClient = new SmtpClient(ConfigSystem.Smtp);
                smtpClient.EnableSsl = true;
                smtpClient.Port = ConfigSystem.Port;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential(mailBox.FromEmail, mailBox.Password);
                smtpClient.Send(mailMessage);

                result = await _userRepository.UpdateOTP(email, mailBox.RamdomCode);
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// VerifyOTP
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="codeOTP">CodeOTP</param>
        /// <returns>bool</returns>
        /// CreatedBy: MinhVN(13/01/2023)
        public async Task<OperationResult<bool>> VerifyOTP(string email, string codeOTP)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. Email is null
            if (string.IsNullOrWhiteSpace(email))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailNotEmpty);

                return result;
            }

            // Get userByEmail
            var userByEmail = await _userRepository.GetUserByEmail(email);

            // 2. userByEmail is null
            if (userByEmail.Data is null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailNotFound);

                return result;
            }

            try
            {
                // Check code otp incorrect
                if (userByEmail.Data.CodeOTP != codeOTP)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByCodeOTPIncorrect);

                    return result;
                }

                // Check expired code otp
                if ((DateTime.UtcNow - userByEmail.Data.TimeCreateOTP).TotalSeconds >= 60)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByCodeOTPExpired);

                    return result;
                }

                result.Data = true;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        public async Task<OperationResult<bool>> Delete(int id)
        {
            return await _userRepository.Delete(id);
        }

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(22/12/2022)
        public async Task<OperationResult<bool>> Insert(User user)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. UserName is null
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByNameNotEmpty);

                return result;
            }

            // 2. PhoneNumber is null
            if (string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByPhoneNumberNotEmpty);

                return result;
            }

            // Get userByPhoneNumber
            var userByPhoneNumber = await _userRepository.GetUserByPhoneNumber(user.PhoneNumber);

            // 3. userByPhoneNumber is already exist
            if (userByPhoneNumber.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByPhoneNumberAlreadyExist);

                return result;
            }

            // 4. Email is null
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailNotEmpty);

                return result;
            }

            // Get userByEmail
            var userByEmail = await _userRepository.GetUserByEmail(user.Email);

            // 5. userByEmail is already exist
            if (userByEmail.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailAlreadyExist);

                return result;
            }

            // 6. DecentralizationId is null
            if (user.DecentralizationId == default(int))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.DecentralizationByNameNotEmpty);

                return result;
            }

            // Get decentralizationById
            var decentralizationById = await _decentralizationRepository.GetById(user.DecentralizationId);

            // 7. decentralizationById is null
            if (decentralizationById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.DecentralizationByIdNotFound, user.DecentralizationId));

                return result;
            }

            result = await _userRepository.Insert(user);

            return result;
        }

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="user">User</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Update(int id, User user)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. User is null
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByNameNotEmpty);

                return result;
            }

            // 2. PhoneNumber is null
            if (string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByPhoneNumberNotEmpty);

                return result;
            }

            // Get userByPhoneNumber
            var userByPhoneNumber = await _userRepository.GetUserByPhoneNumber(user.PhoneNumber);

            // 3. userByPhoneNumber is not null
            if (userByPhoneNumber.Data is not null)
            {
                // 3.1 PhoneNumber is already exist
                if (userByPhoneNumber.Data.PhoneNumber.ToLower().Trim().Equals(user.PhoneNumber.ToLower().Trim())
                    && userByPhoneNumber.Data.UserId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByPhoneNumberAlreadyExist);

                    return result;
                }
            }

            // 4. Email is null
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailNotEmpty);

                return result;
            }

            // Get userByEmail
            var userByEmail = await _userRepository.GetUserByEmail(user.Email);

            // 5. userByEmail is not null
            if (userByEmail.Data is not null)
            {
                // 5.1 Email is already exist
                if (userByEmail.Data.Email.ToLower().Trim().Equals(user.Email.ToLower().Trim())
                    && userByEmail.Data.UserId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailAlreadyExist);

                    return result;
                }
            }

            // 6. DecentralizationId is null
            if (user.DecentralizationId == default(int))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.DecentralizationByNameNotEmpty);

                return result;
            }

            // Get decentralizationById
            var decentralizationById = await _decentralizationRepository.GetById(user.DecentralizationId);

            // 7. decentralizationById is null
            if (decentralizationById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.DecentralizationByIdNotFound, user.DecentralizationId));

                return result;
            }

            result = await _userRepository.Update(id, user);

            return result;
        }

        /// <summary>
        /// UpdatePassword
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="password">Password</param>
        /// <returns>bool</returns>
        /// CreatedBy: MinhVN(15/01/2023)
        public async Task<OperationResult<bool>> UpdatePassword(int id, string password)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. Password is null
            if (string.IsNullOrWhiteSpace(password))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByPasswordNotEmpty);

                return result;
            }

            result = await _userRepository.UpdatePassword(id, password);

            return result;
        }

        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>bool</returns>
        /// CreatedBy: MinhVN(15/01/2023)
        public async Task<OperationResult<bool>> ChangePassword(string email, string password)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. Password is null
            if (string.IsNullOrWhiteSpace(password))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByPasswordNotEmpty);

                return result;
            }

            // 2. Email is null
            if (string.IsNullOrWhiteSpace(email))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailNotEmpty);

                return result;
            }

            result = await _userRepository.ChangePassword(email, password);

            return result;
        }

        /// <summary>
        /// RandomCodeOTP
        /// </summary>
        /// <returns>string</returns>
        private string RandomCodeOTP()
        {
            Random random = new Random();
            return (random.Next(100000, 999999)).ToString();
        }
    }
}