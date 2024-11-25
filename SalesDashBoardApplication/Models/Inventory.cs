using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        [Required]
        public int StockLevel { get; set; }

        [Required]
        public int ReorderLevel { get; set; }


        public int? ProductId { get; set; }
        public Product? Product { get; set; }

    }
}
