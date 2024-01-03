using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Options;
using VnEdu.Core.Entities.Results;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Services;
using VnEdu.Infrastructure.Commom;
using VnEdu.Infrastructure.Data;
using VnEdu.Infrastructure.Repositories.Commom;

namespace VnEdu.Infrastructure.Repositories
{
    /// <summary>
    /// Information of UserRepository
    /// CreatedBy: MinhVN(22/12/2022)
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        private readonly IdentityService _identityService;
        private readonly IMapper _mapper;

        public UserRepository(DataContext dataContext, IdentityService identityService, IMapper mapper)
        {
            _dataContext = dataContext;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<OperationResult<bool>> UpdateOTP(string email, string codeOTP)
        {
            var result = new OperationResult<bool>();

            try
            {
                var user = await _dataContext.User
                    .FirstOrDefaultAsync(u => u.Email.Trim().ToLower().Equals(email.Trim().ToLower()));

                // Check user is null
                if (user is null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.UserByEmailNotFound);

                    return result;
                }

                user.CodeOTP = codeOTP;
                user.TimeCreateOTP = DateTime.UtcNow;

                _dataContext.User.Update(user);
                await _dataContext.SaveChangesAsync();

                result.Data = true;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetUserByIdPassword
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="password">Password</param>
        /// <returns>bool</returns>
        /// CreatedBy: MinhVN(01/13/2023)
        public async Task<OperationResult<bool>> GetUserByIdPassword(int id, string password)
        {
            var result = new OperationResult<bool>();

            try
            {
                var userByIdPassword = await _dataContext.User.FirstOrDefaultAsync(u =>
                    u.UserId == id
                    && u.Password.ToLower().Trim().Equals(Convert.ToBase64String(Encoding.ASCII.GetBytes(password)).ToLower().Trim()));

                // Check userByIdPassword is null
                if (userByIdPassword is null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.UserByIdPasswodNotFound);

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
        /// GetUserByEmail
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>User</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<User>> GetUserByEmail(string email)
        {
            var result = new OperationResult<User>();

            try
            {
                var userByEmail = await _dataContext.User.FirstOrDefaultAsync(u =>
                    u.Email.ToLower().Trim().Equals(email.ToLower().Trim()));

                // Check userByEmail is null
                if (userByEmail is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.UserByEmailNotFound, email));

                    return result;
                }

                result.Data = userByEmail;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetUserByPhoneNumber
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <returns>User</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<User>> GetUserByPhoneNumber(string phoneNumber)
        {
            var result = new OperationResult<User>();

            try
            {
                var userByPhoneNumber = await _dataContext.User.FirstOrDefaultAsync(u =>
                    u.PhoneNumber.ToLower().Trim().Equals(phoneNumber.ToLower().Trim()));

                // Check userByPhoneNumber is null
                if (userByPhoneNumber is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.UserByPhoneNumberNotFound, phoneNumber));

                    return result;
                }

                result.Data = userByPhoneNumber;
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
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Delete(int id)
        {
            var result = new OperationResult<bool>();

            try
            {
                var userById = await _dataContext.User.FirstOrDefaultAsync(u => u.UserId == id);

                // Check userById is null
                if (userById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.UserByIdNotFound, id));

                    return result;
                }

                _dataContext.User.Remove(userById);
                await _dataContext.SaveChangesAsync();

                result.Data = true;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<IEnumerable<object>>> GetAll()
        {
            var result = new OperationResult<IEnumerable<object>>();

            try
            {
                var users = await _dataContext.User
                    .Join(_dataContext.Decentralization,
                        u => u.DecentralizationId,
                        d => d.DecentralizationId,
                        (u, d) => new
                        {
                            u.UserId,
                            u.UserName,
                            u.PhoneNumber,
                            u.Email,
                            u.DecentralizationId,
                            d.DecentralizationName
                        })
                    .ToListAsync();

                result.Data = _mapper.Map<IEnumerable<object>>(users);
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetAllPaging
        /// </summary>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="pageIndex">PageIndex</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>PagingResult</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<PagingResult<object>>> GetAllPaging(string? valueFilter, int pageIndex, int pageSize)
        {
            var result = new OperationResult<PagingResult<object>>();

            if (pageIndex <= 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.PageIndex);

                return result;
            }
            if (pageSize <= 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.PageSize);

                return result;
            }

            try
            {
                var usersPaging = new List<object>();
                var users = new List<object>();

                if (!string.IsNullOrWhiteSpace(valueFilter))
                {
                    var userDb = await _dataContext.User
                        .Join(_dataContext.Decentralization,
                            u => u.DecentralizationId,
                            d => d.DecentralizationId,
                            (u, d) => new
                            {
                                u.UserId,
                                u.UserName,
                                u.PhoneNumber,
                                u.Email,
                                u.DecentralizationId,
                                d.DecentralizationName
                            })
                        .Where(u => u.UserName.ToLower().Trim().Contains(valueFilter.ToLower().Trim())
                            || u.PhoneNumber.ToLower().Trim().Contains(valueFilter.ToLower().Trim())
                            || u.Email.ToLower().Trim().Contains(valueFilter.ToLower().Trim()))
                        .ToListAsync();

                    users = _mapper.Map<List<object>>(userDb);

                    usersPaging = users
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }
                else
                {
                    var userDB = await _dataContext.User
                       .Join(_dataContext.Decentralization,
                           u => u.DecentralizationId,
                           d => d.DecentralizationId,
                           (u, d) => new
                           {
                               u.UserId,
                               u.UserName,
                               u.PhoneNumber,
                               u.Email,
                               u.DecentralizationId,
                               d.DecentralizationName
                           })
                       .ToListAsync();

                    users = _mapper.Map<List<object>>(userDB);

                    usersPaging = users
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }

                var toTalRecord = users.Count;
                var toTalPage = (toTalRecord % pageSize) == 0 ? ((int)toTalRecord / (int)pageSize) : ((int)toTalRecord / (int)pageSize + 1);

                var pagingResult = new PagingResult<object>()
                {
                    ToTalPage = toTalPage,
                    ToTalRecord = toTalRecord,
                    Data = usersPaging
                };

                result.Data = pagingResult;

                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>object</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<object>> GetById(int id)
        {
            var result = new OperationResult<object>();

            try
            {
                var userById = await _dataContext.User
                    .Join(_dataContext.Decentralization,
                        u => u.DecentralizationId,
                        d => d.DecentralizationId,
                        (u, d) => new
                        {
                            u.UserId,
                            u.UserName,
                            u.PhoneNumber,
                            u.Email,
                            u.DecentralizationId,
                            d.DecentralizationName
                        })
                    .FirstOrDefaultAsync(u => u.UserId == id);

                // Check userById is null
                if (userById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.UserByIdNotFound, id));

                    return result;
                }

                result.Data = _mapper.Map<object>(userById);
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetUserByEmailPassword
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Token</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<string>> GetUserByEmailPassword(string email, string password)
        {
            var result = new OperationResult<string>();

            try
            {
                var userByEmailPassword = await _dataContext.User.FirstOrDefaultAsync(u =>
                    u.Email.ToLower().Trim().Equals(email.ToLower().Trim())
                    && u.Password.ToLower().Trim().Equals(Convert.ToBase64String(Encoding.ASCII.GetBytes(password)).ToLower().Trim()));

                // Check userByEmailPassword is null
                if (userByEmailPassword is null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.UserByNotLogin);

                    return result;
                }

                result.Data = GetJwtString(userByEmailPassword);
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetUserByPhoneNumberPassword
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <param name="password">Password</param>
        /// <returns>Token</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<string>> GetUserByPhoneNumberPassword(string phoneNumber, string password)
        {
            var result = new OperationResult<string>();

            try
            {
                var userByPhoneNumberPassword = await _dataContext.User.FirstOrDefaultAsync(u =>
                    u.PhoneNumber.ToLower().Trim().Equals(phoneNumber.ToLower().Trim())
                    && u.Password.ToLower().Trim().Equals(Convert.ToBase64String(Encoding.ASCII.GetBytes(password)).ToLower().Trim()));

                // Check userByPhoneNumberPassword is null
                if (userByPhoneNumberPassword is null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.UserByNotLogin);

                    return result;
                }

                result.Data = GetJwtString(userByPhoneNumberPassword);
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> Insert(User user)
        {
            var result = new OperationResult<bool>();

            try
            {
                var userNew = new User()
                {
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    Password = Convert.ToBase64String(Encoding.ASCII.GetBytes(ConfigSystem.PasswordDefault)),
                    DecentralizationId = user.DecentralizationId
                };

                _dataContext.User.Add(userNew);
                await _dataContext.SaveChangesAsync();

                result.Data = true;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

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

            try
            {
                var userById = await _dataContext.User.FirstOrDefaultAsync(u => u.UserId == id);

                // Check userById is null
                if (userById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.UserByIdNotFound, id));

                    return result;
                }

                userById.UserName = user.UserName;
                userById.PhoneNumber = user.PhoneNumber;
                userById.Email = user.Email;
                userById.DecentralizationId = user.DecentralizationId;

                _dataContext.User.Update(userById);
                await _dataContext.SaveChangesAsync();

                result.Data = true;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// UpdatePassword
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="password">Password</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        public async Task<OperationResult<bool>> UpdatePassword(int id, string password)
        {
            var result = new OperationResult<bool>();

            try
            {
                var userById = await _dataContext.User.FirstOrDefaultAsync(u => u.UserId == id);

                // Check userById is null
                if (userById is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.UserByIdNotFound, id));

                    return result;
                }

                userById.Password = Convert.ToBase64String(Encoding.ASCII.GetBytes(password));

                _dataContext.User.Update(userById);
                await _dataContext.SaveChangesAsync();

                result.Data = true;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetJwtString
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Token</returns>
        /// CreatedBy: MinhVN(23/12/2022)
        private string GetJwtString(User user)
        {
            var claimIndetity = new ClaimsIdentity(new Claim[]
               {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("UserName", user.UserName),
                    new Claim("PhoneNumber", user.PhoneNumber),
                    new Claim("Email", user.Email)
               });

            var token = _identityService.CreateSecurityToken(claimIndetity);
            return _identityService.WriteToken(token);
        }

        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(15/01/2023)
        public async Task<OperationResult<bool>> ChangePassword(string email, string password)
        {
            var result = new OperationResult<bool>();

            try
            {
                var userByEmail = await _dataContext.User
                    .FirstOrDefaultAsync(u => u.Email.Trim().ToLower().Equals(email.Trim().ToLower()));

                // Check userByEmail is null
                if (userByEmail is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.UserByEmailNotFound, email));

                    return result;
                }

                userByEmail.Password = Convert.ToBase64String(Encoding.ASCII.GetBytes(password));

                _dataContext.User.Update(userByEmail);
                await _dataContext.SaveChangesAsync();

                result.Data = true;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }
    }
}