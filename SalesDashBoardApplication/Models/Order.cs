using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        public double OrderAmount { get; set; }

        [Required]
        [MaxLength(32)]
        public string TransactionReference { get; set; }


        public int? UserId { get; set; }
        public User? User { get; set; }


        public ICollection<OrderItem>? OrderItems { get; set; }


        public Order()
        {
            TransactionReference = Guid.NewGuid().ToString("N");
        }
    }
}
