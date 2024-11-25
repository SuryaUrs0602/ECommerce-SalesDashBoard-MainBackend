using SalesDashBoardApplication.Models.DTO.RevenueDto;

namespace SalesDashBoardApplication.Repositories.Contracts
{
    public interface IRevenueRepository
    {
        Task<IEnumerable<TotalRevenueDto>> RevenueInYear(int year);
        Task<IEnumerable<TotalRevenueDto>> RevenueInRange(int startYear, int endYear);



        Task<IEnumerable<RevenuePerOrderDto>> RevenuePerOrderInYear(int year);
        Task<IEnumerable<RevenuePerOrderDto>> RevenuePerOrderRange(int startYear, int endYear);



        Task<IEnumerable<RevenueGrowthRateDto>> RevenueGrowthInYear(int year);
        Task<IEnumerable<RevenueGrowthRateDto>> RevenueGrowthInRange(int startYear, int endYear);



        Task<IEnumerable<TotalCostDto>> TotalCostOfYear(int year);
        Task<IEnumerable<TotalCostDto>> TotalCostInRange(int startYear, int endYear);



        Task<IEnumerable<CostPerOrderDto>> CostPerOrderInYear(int year);
        Task<IEnumerable<CostPerOrderDto>> CostPerOrderInRange(int startYear, int endYear);
    }
}
