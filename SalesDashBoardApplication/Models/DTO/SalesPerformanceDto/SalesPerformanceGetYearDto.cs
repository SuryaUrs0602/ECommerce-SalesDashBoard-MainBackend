using System.ComponentModel.DataAnnotations;

namespace SalesDashBoardApplication.Models.DTO.SalesPerformanceDto
{
    public class SalesPerformanceGetYearDto
    {

        public int TotalOrders { get; set; }


        public double AverageOrderValue { get; set; }


        public int CountOfOrderedUser { get; set; }

 
        public int CountOfusers { get; set; }


        public int CountOfUnitSold { get; set; }


        public double SalesGrowthRate { get; set; }
    }
}
