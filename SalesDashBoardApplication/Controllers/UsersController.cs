using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Models.DTO.UserDto;
using SalesDashBoardApplication.Services.Contracts;

namespace SalesDashBoardApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }




        /// <summary>
        /// Registers a new user with the provided registration details
        /// </summary>
        /// <param name="userRegisterDto">The data transfer object containing the user registration information</param>
        /// <returns>A task representing the asynchronous operation</returns>

        [HttpPost("register")]
        public async Task Register(UserRegisterDto userRegisterDto)
        {
            var user = new User
            {
                UserName = userRegisterDto.UserName,
                UserEmail = userRegisterDto.UserEmail,
                UserPassword = userRegisterDto.UserPassword,
                UserRole = userRegisterDto.UserRole
            };

            _logger.LogInformation("Registering a new user");
            await _userService.RegisterUser(user);
        }



        /// <summary>
        /// Authenticates the user and returns the token if the credentials are valid
        /// </summary>
        /// <param name="loginDto">The data transfer object containing the user login credentials</param>
        /// <returns>A task representing the asynchronous operation, containing user information and authentication token.</returns>

        [HttpPost("login")]
        public async Task<UserWithTokenDto> Login(UserLoginDto loginDto)
        {
            _logger.LogInformation("Logging in the user");
            return await _userService.Login(loginDto.UserEmail, loginDto.UserPassword);
        }



        /// <summary>
        /// Retrieves a list of all registered users.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing an List of user data</returns> 

        [HttpGet]
        public async Task<IEnumerable<UserGetDto>> GetAllUsers()
        {
            _logger.LogInformation("Getting all user data");
            return await _userService.GetAllUsers();
        }




        /// <summary>
        /// Retrieves the user data for a specific user identified by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user to retrieve</param>
        /// <returns>A task representing the asynchronous operation, containing the user details associated with the specific ID</returns>

        [HttpGet("{id}")]
        public async Task<User> GetUserById(int id)
        {
            _logger.LogInformation("Getting user data by id");
            return await _userService.GetUserById(id);
        }




        /// <summary>
        /// Changes the password for a user identified by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user whose password is to be changed.</param>
        /// <param name="userPasswordChangeDto">The data transfer object containing the new password</param>
        /// <returns>A task representing the asynchronous operation</returns>

        [HttpPost("{id}/change-password")]
        public async Task ChangePassword(int id, UserPasswordChangeDto userPasswordChangeDto)
        {
            _logger.LogInformation("Changing the password of the user");
            await _userService.ChangePassword(id, userPasswordChangeDto.UserPassword);
        }




        /// <summary>
        /// Applies partial updates to a user's details identified by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user to update</param>
        /// <param name="patchDocument">he JSON Patch document containing the updates to apply.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ApplicationException">Thrown when the patch document is null, the user cannot be found, or the update fails.</exception>

        [HttpPatch("{id}")]
        public async Task UpdateUser(int id, JsonPatchDocument<UserUpdateDto> patchDocument)
        {
            if (patchDocument == null)
                throw new ApplicationException("Could not update user try again later"); 

            var existingUser = await _userService.GetUserById(id);

            if (existingUser == null)
                throw new ApplicationException("Could not fetch user data to update user try again later");

            var userUpdateDto = new UserUpdateDto
            {
                UserName = existingUser.UserName,
                UserEmail = existingUser.UserEmail
            };

            patchDocument.ApplyTo(userUpdateDto, ModelState);

            if (!ModelState.IsValid)
                throw new ApplicationException("Could not update user try again later after sometimes");

            existingUser.UserName = userUpdateDto.UserName;
            existingUser.UserEmail = userUpdateDto.UserEmail;

            _logger.LogInformation("Updating the user details");
            await _userService.UpdateUser(existingUser);
        }

    }
}
