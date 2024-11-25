using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Models.DTO.UserDto;
using SalesDashBoardApplication.Repositories.Contracts;
using SalesDashBoardApplication.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace SalesDashBoardApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task ChangePassword(int id, string newPassword)
        {
            try
            {
                _logger.LogInformation("Attempting to change the password of user");
                var user = await _userRepository.GetUserById(id);
                if (user != null)
                {
                    user.UserPassword = newPassword;
                    _logger.LogInformation("Calling the update method to change password");
                    await _userRepository.UpdateUser(user);
                }
                else
                {
                    _logger.LogWarning("User with ID {Id} not found during password change attempt", id);
                }
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "Argument null exception while changing password for user ID {Id}.", id);
                throw new Exception("A required argument was missing during password change.");
            }

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error occurred while changing password for user ID {Id}.", id);
                throw new Exception("An error occurred while accessing the database during password change.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while changing password for user ID {Id}.", id);
                throw new Exception("An unexpected error occurred while changing password. Please try again later.");
            }
        }

        public async Task DeleteUser(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to delete the user");
                await _userRepository.DeleteUser(id);
            }

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error occurred while deleting user ID {Id}.", id);
                throw new Exception("An error occurred while accessing the database during user deletion.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while deleting user ID {Id}.", id);
                throw new Exception("An unexpected error occurred while deleting the user. Please try again later.");
            }
        }

        public async Task<IEnumerable<UserGetDto>> GetAllUsers()
        {
            try
            {
                _logger.LogInformation("Attempting to fetch all the user");
                var users = await _userRepository.GetAllUsers();
                return users.Select(x => new UserGetDto
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    UserEmail = x.UserEmail,
                    UserRole = x.UserRole
                });
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching all users");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching all users.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all users.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }

        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch the user by id");
                return await _userRepository.GetUserById(id);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching particular product");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching particular product.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching particular product.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<UserWithTokenDto> Login(string useremail, string password)
        {
            try
            {
                _logger.LogInformation("Attempting to Login the user");
                var user = await _userRepository.GetUserByEmail(useremail);
                if (user != null)
                {
                    if (user.UserEmail == useremail && user.UserPassword == password)
                    {
                        _logger.LogInformation("Login successfull");

                        var token = GenerateJwtToken(user);

                        _logger.LogInformation($"The token generated is {token}");

                        var userWithTokenDto = new UserWithTokenDto
                        {
                            UserId = user.UserId,
                            UserName = user.UserName,
                            UserEmail = user.UserEmail,
                            UserRole = user.UserRole,
                            Token = token
                        };

                        return userWithTokenDto;
                    }
                }
                _logger.LogWarning("Login failed for user with email {Email}", useremail);
                return null;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred during login for user with email {Email}.", useremail);
                throw new Exception("An unexpected error occurred during login. Please try again later.");
            }
        }

        public async Task RegisterUser(User user)
        {
            try
            {
                _logger.LogInformation("Attempting to Register the new user");
                await _userRepository.RegisterUser(user);
            }

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "A database update error occurred while adding a user.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while adding a user.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding a user.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task UpdateUser(User user)
        {
            try
            {
                _logger.LogInformation("Attempting to Update the user");
                await _userRepository.UpdateUser(user);
            }

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "A database update error occurred while updating a user.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while updating a user.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating a user.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }



        private string GenerateJwtToken(User user)
        {
            _logger.LogInformation("Generating token for the user");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.UserEmail)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
                expires:  DateTime.Now.AddMinutes(10), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }


    }
}
