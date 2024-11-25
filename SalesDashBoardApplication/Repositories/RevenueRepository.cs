using Microsoft.EntityFrameworkCore;
using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Models.DTO.RevenueDto;
using SalesDashBoardApplication.Repositories.Contracts;

namespace SalesDashBoardApplication.Repositories
{
    public class RevenueRepository : IRevenueRepository
    {
        private readonly SalesDashBoardDbContext _context;
        private readonly ILogger<RevenueRepository> _logger;

        public RevenueRepository(SalesDashBoardDbContext context, ILogger<RevenueRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<CostPerOrderDto>> CostPerOrderInRange(int startYear, int endYear)
        {
            _logger.LogInformation($"Fetching the cost per order between the year {startYear} and {endYear}");
            return await _context.Revenues
                .Where(sp => sp.Date.Year >= startYear && sp.Date.Year <= endYear)
                .GroupBy(sp => sp.Date.Year)
                .Select(x => new CostPerOrderDto
                {
                    AverageCostPerOrder = x.Sum(x => x.AverageCostPerOrder)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CostPerOrderDto>> CostPerOrderInYear(int year)
        {
            _logger.LogInformation($"Attempting to fetch Cost per order of a year {year}");
            return await _context.Revenues
                .Where(sp => sp.Date.Year == year)
                .GroupBy(sp => sp.Date.Month)
                .Select(x => new CostPerOrderDto
                {
                    AverageCostPerOrder = x.Sum(x => x.AverageCostPerOrder)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<RevenueGrowthRateDto>> RevenueGrowthInRange(int startYear, int endYear)
        {
            _logger.LogInformation($"Fetching the revenue growth rate between the years {startYear} and {endYear}");
            return await _context.Revenues
                .Where(sp => sp.Date.Year >= startYear && sp.Date.Year <= endYear)
                .GroupBy(sp => sp.Date.Year)
                .Select(x => new  RevenueGrowthRateDto
                {
                    RevenueGrowthRate = x.Sum(x => x.RevenueGrowthRate)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<RevenueGrowthRateDto>> RevenueGrowthInYear(int year)
        {
            _logger.LogInformation($"Fetching the revenue growth rate of the year {year}");
            return await _context.Revenues
                .Where(sp => sp.Date.Year == year)
                .GroupBy(sp => sp.Date.Month)
                .Select(x => new RevenueGrowthRateDto
                {
                    RevenueGrowthRate = x.Sum(x => x.RevenueGrowthRate)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TotalRevenueDto>> RevenueInRange(int startYear, int endYear)
        {
            _logger.LogInformation($"Fetching the revenue data btween the year {startYear} and {endYear}");
            return await _context.Revenues
                .Where(sp => sp.Date.Year >= startYear && sp.Date.Year <= endYear)
                .GroupBy(sp => sp.Date.Year)
                .Select(x => new TotalRevenueDto
                {
                    TotalRevenue = x.Sum(x => x.TotalRevenue)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TotalRevenueDto>> RevenueInYear(int year)
        {
            _logger.LogInformation($"fetching revenue of the year {year}");
            return await _context.Revenues
                .Where(sp => sp.Date.Year == year)
                .GroupBy(sp => sp.Date.Month)
                .Select(x => new TotalRevenueDto
                {
                    TotalRevenue = x.Sum(x => x.TotalRevenue)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<RevenuePerOrderDto>> RevenuePerOrderInYear(int year)
        {
            _logger.LogInformation($"Fetching the revenue per order data of year {year}");
            return await _context.Revenues
                .Where(sp => sp.Date.Year == year)
                .GroupBy (sp => sp.Date.Month)
                .Select (x => new RevenuePerOrderDto
                {
                    AverageRevenuePerOrder = x.Sum(x => x.AverageRevenuePerOrder)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<RevenuePerOrderDto>> RevenuePerOrderRange(int startYear, int endYear)
        {
            _logger.LogInformation($"Fetching the revenue per order betwenn the year {startYear} and {endYear}");
            return await _context.Revenues
                .Where(sp => sp.Date.Year >= startYear && sp.Date.Year <= endYear)
                .GroupBy(sp => sp.Date.Year)
                .Select(x => new RevenuePerOrderDto
                {
                    AverageRevenuePerOrder = x.Sum(x => x.AverageRevenuePerOrder)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TotalCostDto>> TotalCostInRange(int startYear, int endYear)
        {
            _logger.LogInformation($"Fetching the total cost invested between the year {startYear} and {endYear}");
            return await _context.Revenues
                .Where(sp => sp.Date.Year >= startYear && sp.Date.Year <= endYear)
                .GroupBy (sp => sp.Date.Year)
                .Select(x => new TotalCostDto
                {
                    TotalCost = x.Sum(x => x.TotalCost)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TotalCostDto>> TotalCostOfYear(int year)
        {
            _logger.LogInformation($"fetching the total cost invested in a year {year}");
            return await _context.Revenues
                .Where(sp => sp.Date.Year == year)
                .GroupBy(sp => sp.Date.Month)
                .Select(x => new TotalCostDto
                {
                    TotalCost = x.Sum(x => x.TotalCost)
                })
                .ToListAsync();
        }
    }
}
