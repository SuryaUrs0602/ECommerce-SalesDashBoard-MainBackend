using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models.DTO.UserDto
{
    public class UserWithTokenDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public UserRole UserRole { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
