using SalesDashBoardApplication.Models.DTO.RevenueDto;
using SalesDashBoardApplication.Repositories.Contracts;
using SalesDashBoardApplication.Services.Contracts;
using System.Text.RegularExpressions;

namespace SalesDashBoardApplication.Services
{
    public class RevenueService : IRevenueService
    {
        private readonly IRevenueRepository _revenueRepository;
        private readonly ILogger<RevenueService> _logger;

        public RevenueService(IRevenueRepository revenueRepository, ILogger<RevenueService> logger)
        {
            _revenueRepository = revenueRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CostPerOrderDto>> CostPerOrderInRange(int startYear, int endYear)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch cost per order between the range of years");
                return await _revenueRepository.CostPerOrderInRange(startYear, endYear);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching cost per order between the range of years");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching cost per order between the range of years");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching cost per order between the range of years");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<CostPerOrderDto>> CostPerOrderInYear(int year)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch cost per order of a year");
                return await _revenueRepository.CostPerOrderInYear(year);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching cost per order of a year");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching cost per order of a year.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching cost per order of a year.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<RevenueGrowthRateDto>> RevenueGrowthInRange(int startYear, int endYear)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch Revenue Growth between years");
                return await _revenueRepository.RevenueGrowthInRange(startYear, endYear);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching Revenue Growth between years");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching Revenue Growth between years.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching Revenue Growth between years.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<RevenueGrowthRateDto>> RevenueGrowthOfYear(int year)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch revenue growth of a year");
                return await _revenueRepository.RevenueGrowthInYear(year);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching revenue growth of a year");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching revenue growth of a year.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching revenue growth of a year.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<TotalRevenueDto>> RevenueInRange(int startYear, int endYear)
        {
            try
            {
                _logger.LogInformation("Attempting to fecth revenue data in range of years");
                return await _revenueRepository.RevenueInRange(startYear, endYear);
            }
            
            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching revenue data in range of years");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching revenue data in range of years.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching revenue data in range of years.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<TotalRevenueDto>> RevenueInYear(int year)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch Revenue of the particular year");
                return await _revenueRepository.RevenueInYear(year);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching Revenue of the particular year");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching Revenue of the particular year.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching Revenue of the particular year.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<RevenuePerOrderDto>> RevenuePerOrderInRange(int startYear, int endYear)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch the revenue per order in range of years");
                return await _revenueRepository.RevenuePerOrderRange(startYear, endYear);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching Revenue per order in range of years");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching Revenue per order in range of years.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching Revenue per order in range of years.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<RevenuePerOrderDto>> RevenuePerOrderInYear(int year)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch the revenue per order of the year");
                return await _revenueRepository.RevenuePerOrderInYear(year);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching Revenue per order of the year");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching Revenue per order of the year.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching Revenue per order of the year.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<TotalCostDto>> TotalCostInRange(int startYear, int endYear)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch total cost invested in range of years");
                return await _revenueRepository.TotalCostInRange(startYear, endYear);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching Total cost invested in range of years");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching Total cost invested in range of years.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching Total cost invested in range of years.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<TotalCostDto>> TotalCostOfYear(int year)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch Total cost invested in a year");
                return await _revenueRepository.TotalCostOfYear(year);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching Total cost invested in a year");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching Total cost invested in a year.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching Total cost invested in a year.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }
    }
}
