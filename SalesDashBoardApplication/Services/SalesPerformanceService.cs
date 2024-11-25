using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Models.DTO.SalesPerformanceDto;
using SalesDashBoardApplication.Repositories.Contracts;
using SalesDashBoardApplication.Services.Contracts;

namespace SalesDashBoardApplication.Services
{
    public class SalesPerformanceService : ISalesPerformanceService
    {
        private readonly ISalesPerformanceRepository _salesPerformanceRepository;
        private readonly ILogger<SalesPerformanceService> _logger;

        public SalesPerformanceService(ISalesPerformanceRepository salesPerformanceRepository, ILogger<SalesPerformanceService> logger)
        {
            _salesPerformanceRepository = salesPerformanceRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<AverageOrderValueDto>> AOVInAYear(int year)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch all the aov of a year");
                return await _salesPerformanceRepository.AOVInAYear(year);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching all aov of a year");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching all aov of a year.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all aov of a year.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public Task<IEnumerable<AverageOrderValueDto>> AOVRangeYear(int startYear, int endYear)
        {
            try
            {
                _logger.LogInformation("Attemping to fetch all the aov data between year");
                return _salesPerformanceRepository.AOVRangeYear(startYear, endYear);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching all aov data between year");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching all aov data between year.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all aov data between year.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<CountOfUsersDto>> CountOfUserInAYear(int year)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch all count of users in a year");
                return await _salesPerformanceRepository.CountOfUser(year);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching all count of users in a year");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching all count of users in a year.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all count of users in a year.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<CountOfOrderedUsersDto>> OrderedUserCount(int year)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch Count of Ordered users in a year");
                return await _salesPerformanceRepository.CountOfOrderedUsers(year);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching Count of Ordered users in a year");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching Count of Ordered users in a year.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching Count of Ordered users in a year.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<CountOfOrderedUsersDto>> OrdereduserRange(int startYear, int endYear)
        {
            try
            {
                _logger.LogInformation("Attempting to fetching Ordered User count between years");
                return await _salesPerformanceRepository.OrderedUserRange(startYear, endYear);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching all Ordered User count between years");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching all Ordered User count between years");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all Ordered User count between years");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<PopularProductDto> PopularProductOfYear(int year)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch the most popular product of the year");
                return await _salesPerformanceRepository.PopularProductYear(year);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching the most popular product of the year");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching the most popular product of the year.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching the most popular product of the year.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<SalesGrowthDto>> SalesGrowthInYear(int year)
        {
            try
            {
                _logger.LogInformation("Attempting to fetching sales growth in a year");
                return await _salesPerformanceRepository.SalesGrowthRateInYear(year);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching sales growth in a year");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching sales growth in a year.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching sales growth in a year.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<SalesGrowthDto>> SalesGrowthRange(int startYear, int endYear)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch sales growth between year range");
                return await _salesPerformanceRepository.SalesGrowthRange(startYear, endYear);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching sales growth between year range");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching sales growth between year range.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching sales growth between year range.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<TotalOrdersDto>> TotalOrdersData(DateTime year)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch all total orders data of a year");
                return await _salesPerformanceRepository.TotalOrdersInAYear(year);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching all total orders data of a year");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching all total orders data of a year.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all total orders data of a year.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<TotalOrdersDto>> TotalOrdersInRange(int startYear, int endYear)
        {
            try
            {
                _logger.LogInformation("Attemping to fetch all the Totol orders data between year");
                return await _salesPerformanceRepository.TotalOrdersInRange(startYear, endYear);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching all Totol orders data between year");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching all Totol orders data between year.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all Totol orders data between year.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<UnitSoldDto>> UnitSoldInRange(int startYear, int endYear)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch unit sold data btween year range");
                return await _salesPerformanceRepository.CountOfUnitSoldInRange(startYear, endYear);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching unit sold data btween year range");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching unit sold data btween year range");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching unit sold data btween year range.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<UnitSoldDto>> UnitSoldInYear(int year)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch unit sold data of the year");
                return await _salesPerformanceRepository.CountOfUnitSoldInYear(year);
            }


            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching unit sold data of the year");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching unit sold data of the year");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching unit sold data of the year.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<CountOfUsersDto>> UserRange(int startYear, int endYear)
        {
            try
            {
                _logger.LogInformation("Attempting fetch all users count between years");
                return await _salesPerformanceRepository.CountOfUsersRange(startYear, endYear);
            }


            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching all users count between years");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching all users count between years");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all users count between years.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        
    }
}
