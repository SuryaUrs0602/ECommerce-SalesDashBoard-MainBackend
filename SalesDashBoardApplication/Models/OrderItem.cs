using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double UnitPrice { get; set; }


        public int? OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }

        public int? ProductId { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }
    }
}
