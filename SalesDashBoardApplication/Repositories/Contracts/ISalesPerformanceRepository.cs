using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Models.DTO.SalesPerformanceDto;

namespace SalesDashBoardApplication.Repositories.Contracts
{
    public interface ISalesPerformanceRepository
    {
        //Task<IEnumerable<SalesPerformance>> SalesPerformanceBetweenMonthAndYear(DateTime startDate, DateTime endDate);
        //Task<IEnumerable<SalesPerformanceGetYearDto>> SalesPerformanceOfParticularYear(int year);

        Task<IEnumerable<TotalOrdersDto>> TotalOrdersInAYear(DateTime year);
        Task<IEnumerable<TotalOrdersDto>> TotalOrdersInRange(int startYear, int endYear);


        Task<IEnumerable<AverageOrderValueDto>> AOVInAYear(int year);
        Task<IEnumerable<AverageOrderValueDto>> AOVRangeYear(int startYear, int endYear);


        Task<IEnumerable<CountOfUsersDto>> CountOfUser(int year);
        Task<IEnumerable<CountOfUsersDto>> CountOfUsersRange(int startYear, int endYear);



        Task<IEnumerable<CountOfOrderedUsersDto>> CountOfOrderedUsers(int year);
        Task<IEnumerable<CountOfOrderedUsersDto>> OrderedUserRange(int startYear, int endYear);




        Task<IEnumerable<UnitSoldDto>> CountOfUnitSoldInYear(int year);
        Task<IEnumerable<UnitSoldDto>> CountOfUnitSoldInRange(int startYear, int endYear);




        Task<IEnumerable<SalesGrowthDto>> SalesGrowthRateInYear(int year);
        Task<IEnumerable<SalesGrowthDto>> SalesGrowthRange(int startYear, int endYear);




        Task<PopularProductDto> PopularProductYear(int year);
    }
}
