using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models.DTO.ProductDto
{
    public class ProductGetDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ProductImageUrl { get; set; } = string.Empty;

        public string ProductDescription { get; set; } = string.Empty;

        public string ProductCategory { get; set; } = string.Empty;

        public double ProductPrice { get; set; }
    }
}
