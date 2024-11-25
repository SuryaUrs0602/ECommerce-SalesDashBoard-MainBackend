using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Models.DTO.SalesPerformanceDto;

namespace SalesDashBoardApplication.Services.Contracts
{
    public interface ISalesPerformanceService
    {
        //Task<IEnumerable<SalesPerformance>> GetDataBetweenMonthAndYear(DateTime startDate, DateTime endDate);
        //Task<IEnumerable<SalesPerformanceGetYearDto>> GetSalesDataOfYear(int year);


        Task<IEnumerable<TotalOrdersDto>> TotalOrdersData(DateTime year);
        Task<IEnumerable<TotalOrdersDto>> TotalOrdersInRange(int startYear, int endYear);



        Task<IEnumerable<AverageOrderValueDto>> AOVInAYear(int year);
        Task<IEnumerable<AverageOrderValueDto>> AOVRangeYear(int startYear, int endYear);



        Task<IEnumerable<CountOfUsersDto>> CountOfUserInAYear(int year);
        Task<IEnumerable<CountOfUsersDto>> UserRange(int startYear, int endYear);



        Task<IEnumerable<CountOfOrderedUsersDto>> OrderedUserCount(int year);
        Task<IEnumerable<CountOfOrderedUsersDto>> OrdereduserRange(int startYear, int endYear);



        Task<IEnumerable<UnitSoldDto>> UnitSoldInYear(int year);
        Task<IEnumerable<UnitSoldDto>> UnitSoldInRange(int startYear, int endYear);



        Task<IEnumerable<SalesGrowthDto>> SalesGrowthInYear(int year);
        Task<IEnumerable<SalesGrowthDto>> SalesGrowthRange(int startYear, int endYear);



        Task<PopularProductDto> PopularProductOfYear(int year);
    }
}
