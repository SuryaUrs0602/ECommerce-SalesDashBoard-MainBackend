using Microsoft.EntityFrameworkCore;
using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Models.DTO.SalesPerformanceDto;
using SalesDashBoardApplication.Repositories.Contracts;

namespace SalesDashBoardApplication.Repositories
{
    public class SalesPerformanceRepository : ISalesPerformanceRepository
    {
        private readonly SalesDashBoardDbContext _context;
        private readonly ILogger<SalesPerformanceRepository> _logger;

        public SalesPerformanceRepository(SalesDashBoardDbContext context, ILogger<SalesPerformanceRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<IEnumerable<AverageOrderValueDto>> AOVInAYear(int year)
        {
            _logger.LogInformation($"Attempting to fetch AVO value of year {year} in months");
            return await _context.SalesPerformances
                .Where(sp => sp.Date.Year == year)
                .GroupBy(sp => sp.Date.Month)
                .Select(x => new AverageOrderValueDto
                {
                    AverageOrderValue = Math.Round(x.Sum(x => x.AverageOrderValue), 2)                 
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<AverageOrderValueDto>> AOVRangeYear(int startYear, int endYear)
        {
            _logger.LogInformation($"Attempting to fetch aov between the year {startYear} and {endYear}");
            return await _context.SalesPerformances
                .Where(sp => sp.Date.Year >= startYear && sp.Date.Year <= endYear)
                .GroupBy (sp => sp.Date.Year)
                .Select (x => new AverageOrderValueDto
                {
                    AverageOrderValue = Math.Round(x.Sum(x =>x.AverageOrderValue), 2)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CountOfOrderedUsersDto>> CountOfOrderedUsers(int year)
        {
            _logger.LogInformation($"Fetching count of Ordered users of year {year}");
            return await _context.SalesPerformances
                .Where(sp => sp.Date.Year == year)
                .GroupBy(sp => sp.Date.Month)
                .Select(x => new  CountOfOrderedUsersDto
                {
                    CountOfOrderedUser = x.Sum(x => x.CountOfOrderedUser)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<UnitSoldDto>> CountOfUnitSoldInRange(int startYear, int endYear)
        {
            _logger.LogInformation($"fecthing the nnit sold data between the year range {startYear} and {endYear}");
            return await _context.SalesPerformances
                .Where(sp => sp.Date.Year >= startYear && sp.Date.Year <= endYear)
                .GroupBy(sp => sp.Date.Year)
                .Select(x => new  UnitSoldDto
                {
                    CountOfUnitSold = x.Sum(x => x.CountOfUnitSold)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<UnitSoldDto>> CountOfUnitSoldInYear(int year)
        {
            _logger.LogInformation($"fecthing the count of unit sold in the year {year}");
            return await _context.SalesPerformances
                .Where(sp => sp.Date.Year == year)
                .GroupBy(sp => sp.Date.Month)
                .Select(x => new UnitSoldDto
                {
                    CountOfUnitSold = x.Sum(x => x.CountOfUnitSold)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CountOfUsersDto>> CountOfUser(int year)
        {
            _logger.LogInformation($"Fetching the count of users of a year {year}");
            return await _context.SalesPerformances
                .Where(sp => sp.Date.Year == year)
                .GroupBy(sp => sp.Date.Month)
                .Select(x => new  CountOfUsersDto
                {
                    CountOfusers = x.Sum(x => x.CountOfusers)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CountOfUsersDto>> CountOfUsersRange(int startYear, int endYear)
        {
            _logger.LogInformation($"Attempting to fetch total Count Users between the year {startYear} and {endYear}");
            return await _context.SalesPerformances
                .Where(sp => sp.Date.Year >= startYear && sp.Date.Year <= endYear)
                .GroupBy(sp => sp.Date.Year)
                .Select(x => new CountOfUsersDto
                {
                    CountOfusers = x.Sum (x => x.CountOfusers)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CountOfOrderedUsersDto>> OrderedUserRange(int startYear, int endYear)
        {
            _logger.LogInformation($"Fetching the user Count between year range {startYear} and {endYear}");
            return await _context.SalesPerformances
                .Where(sp => sp.Date.Year >= startYear && sp.Date.Year <= endYear)
                .GroupBy(sp => sp.Date.Year)
                .Select(x => new CountOfOrderedUsersDto
                {
                    CountOfOrderedUser = x.Sum(x => x.CountOfOrderedUser)       
                })
                .ToListAsync();
        }

        public async Task<PopularProductDto> PopularProductYear(int year)
        {
            _logger.LogInformation($"Fetching the most ordered product of the year {year}");

            var mostSoldProduct = await _context.SalesPerformances
                .Where(sp => sp.Date.Year == year)
                .GroupBy(sp => sp.MostOrderedProduct)
                .Select(x => new PopularProductDto
                {
                    MostOrderedProduct = x.Key,
                    OrderCount = x.Count()
                })
                .OrderByDescending(x => x.OrderCount)
                .FirstOrDefaultAsync();

            return mostSoldProduct;           
        }

        public async Task<IEnumerable<SalesGrowthDto>> SalesGrowthRange(int startYear, int endYear)
        {
            _logger.LogInformation($"Fetching the sales growth rate between year {startYear} and {endYear}");
            return await _context.SalesPerformances
                .Where(sp => sp.Date.Year >= startYear && sp.Date.Year <= endYear)
                .GroupBy(sp => sp.Date.Year)
                .Select(x => new SalesGrowthDto
                {
                    SalesGrowthRate = Math.Round(x.Sum(x => x.SalesGrowthRate), 2)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<SalesGrowthDto>> SalesGrowthRateInYear(int year)
        {
            _logger.LogInformation($"Fetching grwoth rate of the year {year}");
            return await _context.SalesPerformances
                .Where(sp => sp.Date.Year == year)
                .GroupBy (sp => sp.Date.Month)
                .Select (x => new SalesGrowthDto
                {
                    SalesGrowthRate = Math.Round(x.Sum(x => x.SalesGrowthRate), 2)
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<TotalOrdersDto>> TotalOrdersInAYear(DateTime year)
        {
            _logger.LogInformation($"Attempting to fetch total orders value of year {year} in months");
            int yearValue = year.Year;
            return await _context.SalesPerformances
                .Where(sp => sp.Date.Year == yearValue)
                .GroupBy(sp => sp.Date.Month)
                .Select(x => new TotalOrdersDto
                {
                    TotalOrders = x.Sum(x => x.TotalOrders)
                }).ToListAsync();
        }


        public async Task<IEnumerable<TotalOrdersDto>> TotalOrdersInRange(int startYear, int endYear)
        {
            _logger.LogInformation($"Attempting to fetch total orders value between the year {startYear} and {endYear}");
            return await _context.SalesPerformances
                .Where(sp => sp.Date.Year >= startYear && sp.Date.Year <= endYear)
                .GroupBy(sp => sp.Date.Year)
                .Select(x => new TotalOrdersDto
                {
                    TotalOrders = x.Sum(x => x.TotalOrders)
                })
                .ToListAsync();
        }

        
    }
}
