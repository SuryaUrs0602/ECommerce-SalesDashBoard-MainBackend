using SalesDashBoardApplication.Models;

namespace SalesDashBoardApplication.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task RegisterUser(User user); 
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task UpdateUser(User user);
        Task DeleteUser(int id);
    }
}
