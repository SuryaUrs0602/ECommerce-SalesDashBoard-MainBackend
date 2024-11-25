using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models.DTO.InventoryDto
{
    public class InventoryGetDto
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;
        public int StockLevel { get; set; }
        public double ProductPrice { get; set; }

    }
}
