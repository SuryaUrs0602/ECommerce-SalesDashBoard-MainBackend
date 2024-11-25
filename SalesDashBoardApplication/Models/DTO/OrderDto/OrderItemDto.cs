using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models.DTO.OrderDto
{
    public class OrderItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double UnitPrice { get; set; }
    }
}
