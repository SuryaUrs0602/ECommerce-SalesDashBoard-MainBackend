using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models
{
    public class SalesPerformance
    {
        [Key]
        public int SalesPerformanceId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int TotalOrders { get; set; }

        [Required]
        public double AverageOrderValue { get; set; }           

        [Required]
        public int CountOfOrderedUser { get; set; }

        [Required]
        public int CountOfusers { get; set; }

        [Required]
        public int CountOfUnitSold { get; set; }

        [Required]
        public double SalesGrowthRate { get; set; }

        [Required]
        public string MostOrderedProduct { get; set; } = string.Empty;

    }
}
