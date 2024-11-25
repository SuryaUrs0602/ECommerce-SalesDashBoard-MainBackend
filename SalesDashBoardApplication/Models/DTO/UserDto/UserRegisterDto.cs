using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models.DTO.UserDto
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "This Field cannot be Empty")]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;


        [Required(ErrorMessage = "This Field cannot be Empty")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string UserEmail { get; set; } = string.Empty;


        [Required(ErrorMessage = "This Field cannot be Empty")]
        [MinLength(8)]
        public string UserPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "User Role is Required!!!")]
        public UserRole UserRole { get; set; }
    }
}
