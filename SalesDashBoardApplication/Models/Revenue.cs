using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models
{
    public class Revenue
    {
        [Key]
        public int RevenueId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double TotalRevenue { get; set; }

        [Required]
        public double TotalCost { get; set; }

        [Required]
        public double AverageRevenuePerOrder { get; set; }

        [Required]
        public double AverageCostPerOrder { get; set; }

        [Required]
        public double RevenueGrowthRate { get; set; }               

    }
}
