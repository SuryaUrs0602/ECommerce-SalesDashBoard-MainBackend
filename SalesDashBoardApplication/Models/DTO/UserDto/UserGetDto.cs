using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models.DTO.UserDto
{
    public class UserGetDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;

        public UserRole UserRole { get; set; }
    }
}
