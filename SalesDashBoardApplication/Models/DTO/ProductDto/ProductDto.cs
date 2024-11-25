using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models.DTO.ProductDto
{
    public class ProductDto
    {
        [Required(ErrorMessage = "This Field cannot be Empty")]
        [StringLength(50)]
        public string ProductName { get; set; } = string.Empty;


        [Required(ErrorMessage = "This Field cannot be Empty")]
        public string ProductImageUrl { get; set; } = string.Empty;


        [Required(ErrorMessage = "This Field cannot be Empty")]
        public string ProductDescription { get; set; } = string.Empty;


        [Required(ErrorMessage = "This Field cannot be Empty")]
        [StringLength(50)]
        public string ProductCategory { get; set; } = string.Empty;


        [Required(ErrorMessage = "This Field cannot be Empty")]
        public double ProductPrice { get; set; }
    }
}
