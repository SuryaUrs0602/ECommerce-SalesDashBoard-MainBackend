using Microsoft.EntityFrameworkCore;
using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Repositories.Contracts;

namespace SalesDashBoardApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SalesDashBoardDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(SalesDashBoardDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task DeleteUser(int id)
        {
            var user = await GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Deleted the User with ID {id}");
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            _logger.LogInformation("Fetching all Users Data");
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            _logger.LogInformation($"Fetching User with email {email}");
            return await _context.Users.FirstOrDefaultAsync(user => user.UserEmail == email);
        }

        public async Task<User> GetUserById(int id)
        {
            _logger.LogInformation($"Fetching User with ID {id}");
            return await _context.Users.FindAsync(id);
        }

        public async Task RegisterUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Add a new User with details {user}");
        }

        public async Task UpdateUser(User user)         
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Updated User {user}");
        }
    }
}
