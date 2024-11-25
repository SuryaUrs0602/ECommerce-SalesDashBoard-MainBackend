using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models.DTO.UserDto
{
    public class UserPasswordChangeDto
    {
        [Required(ErrorMessage = "This Field cannot be Empty")]
        [MinLength(8)]
        public string UserPassword { get; set; } = string.Empty;
    }
}
