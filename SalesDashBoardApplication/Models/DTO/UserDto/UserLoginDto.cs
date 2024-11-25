using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models.DTO.UserDto
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "This Field cannot be Empty")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string UserEmail { get; set; } = string.Empty;


        [Required(ErrorMessage = "This Field cannot be Empty")]
        [MinLength(8)]
        public string UserPassword { get; set; } = string.Empty;

    }
}
