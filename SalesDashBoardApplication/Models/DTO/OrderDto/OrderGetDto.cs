using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models.DTO.OrderDto
{
    public class OrderGetDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderAmount { get; set; }
        public string TransactionReference { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public List<OrderItemGetDto> OrderItems { get; set; } = new List<OrderItemGetDto>();

    }
}
