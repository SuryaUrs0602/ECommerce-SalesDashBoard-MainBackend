using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Models.DTO.UserDto;

namespace SalesDashBoardApplication.Services.Contracts
{
    public interface IUserService
    {
        Task RegisterUser(User user);   
        Task<UserWithTokenDto> Login(string useremail, string password);
        Task<User> GetUserById(int id);
        Task<IEnumerable<UserGetDto>> GetAllUsers();
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task ChangePassword(int id, string newPassword);
    }
}
