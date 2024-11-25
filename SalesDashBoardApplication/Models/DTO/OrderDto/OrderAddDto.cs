using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models.DTO.OrderDto
{
    public class OrderAddDto
    {
        [Required]
        public double OrderAmount { get; set; }

        [Required]
        public int UserId { get; set; }

        public List<OrderItemDto>? OrderItems { get; set; }         
    }
}
