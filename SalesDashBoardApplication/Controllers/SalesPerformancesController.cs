using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesDashBoardApplication.Models.DTO.SalesPerformanceDto;
using SalesDashBoardApplication.Services.Contracts;

namespace SalesDashBoardApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPerformancesController : ControllerBase
    {
        private readonly ISalesPerformanceService _salesPerformanceService;
        private readonly ILogger<SalesPerformancesController> _logger;

        public SalesPerformancesController(ISalesPerformanceService salesPerformanceService, ILogger<SalesPerformancesController> logger)
        {
            _salesPerformanceService = salesPerformanceService;
            _logger = logger;
        }


       
        [HttpGet("total-orders/{year}")]
        public async Task<IEnumerable<TotalOrdersDto>> GetTotalOrdersData(int year)
        {
            DateTime selectYear = new DateTime(year, 1, 1);

            _logger.LogInformation("Getting total orders data of a year");
            return await _salesPerformanceService.TotalOrdersData(selectYear);  
        }


        
        [HttpGet("total-orders/{startYear}/{endYear}")]
        public async Task<IEnumerable<TotalOrdersDto>> TotalOrdersRange(int startYear, int endYear)
        {
            _logger.LogInformation("Getting total order in a range of years");
            return await _salesPerformanceService.TotalOrdersInRange(startYear, endYear);
        }


        
        [HttpGet("aov/{year}")]
        public async Task<IEnumerable<AverageOrderValueDto>> AOVInAYear(int year)
        {
            _logger.LogInformation("Getting avergae order value of a year");
            return await _salesPerformanceService.AOVInAYear(year);
        }


        
        [HttpGet("aov/{startYear}/{endYear}")]
        public async Task<IEnumerable<AverageOrderValueDto>> AOVRange(int startYear, int endYear)
        {
            _logger.LogInformation("Getting average order value between the range of years");
            return await _salesPerformanceService.AOVRangeYear(startYear, endYear);
        }



        [HttpGet("users-count/{year}")]
        public async Task<IEnumerable<CountOfUsersDto>> CountOfUsers(int year)
        {
            _logger.LogInformation("Getting the count of users in a year");
            return await _salesPerformanceService.CountOfUserInAYear(year);
        }



        [HttpGet("user-count/{startYear}/{endYear}")]
        public async Task<IEnumerable<CountOfUsersDto>> CountofUserRange(int startYear, int endYear)
        {
            _logger.LogInformation("Getting the count of users in a range of years");
            return await _salesPerformanceService.UserRange(startYear, endYear);
        }



        [HttpGet("ordered-user/{year}")]
        public async Task<IEnumerable<CountOfOrderedUsersDto>> CountOfOrderedUser(int year)
        {
            _logger.LogInformation("Getting count of ordered users of a year");
            return await _salesPerformanceService.OrderedUserCount(year);         
        }



        [HttpGet("ordered-user/{startYear}/{endYear}")]
        public async Task<IEnumerable<CountOfOrderedUsersDto>> CountOfOrderedUserRange(int startYear, int endYear)
        {
            _logger.LogInformation("Getting count of ordered users in a range of years");
            return await _salesPerformanceService.OrdereduserRange(startYear, endYear);                   
        }



        [HttpGet("unit-sold/{year}")]
        public async Task<IEnumerable<UnitSoldDto>> CountOfUnitSold(int year)
        {
            _logger.LogInformation("getting usnit sold in a year");
            return await _salesPerformanceService.UnitSoldInYear(year);
        }



        [HttpGet("unit-sold/{startYear}/{endYear}")]
        public async Task<IEnumerable<UnitSoldDto>> CountOfUnitSoldInRange(int startYear, int endYear)
        {
            _logger.LogInformation("Getting units sold in a range of years");
            return await _salesPerformanceService.UnitSoldInRange(startYear, endYear);
        }



        [HttpGet("sales-growth/{year}")]
        public async Task<IEnumerable<SalesGrowthDto>> SalesGrowthRateInYear(int year)
        {
            _logger.LogInformation("Getting sales growth rate of a year");
            return await _salesPerformanceService.SalesGrowthInYear(year);
        }



        [HttpGet("sales-growth/{startYear}/{endYear}")]
        public async Task<IEnumerable<SalesGrowthDto>> SalesGrowthRange(int startYear, int endYear)
        {
            _logger.LogInformation("Getting sales growth rate between a raneg of years");
            return await _salesPerformanceService.SalesGrowthRange(startYear, endYear);
        }



        [HttpGet("popular-product/{year}")]
        public async Task<PopularProductDto> PopularProduct(int year)
        {
            _logger.LogInformation("Getting Popular product of a year");
            return await _salesPerformanceService.PopularProductOfYear(year);
        }

    }
}
