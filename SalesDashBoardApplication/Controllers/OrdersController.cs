using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Models.DTO.OrderDto;
using SalesDashBoardApplication.Services.Contracts;

namespace SalesDashBoardApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }


        
        /// <summary>
        ///  Retrives the list of all order data
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing an List of order data</returns>
        [HttpGet]
        public async Task<IEnumerable<OrderGetDto>> GetAllOrders()
        {
            _logger.LogInformation("Getting all orders data");
            return await _orderService.GetAllOrders();
        }



        /// <summary>
        /// Retrieves the order data for a specific user identified by their ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the order to retrive</param>
        /// <returns>A task representing the asynchronous operation, containing the order details associated with the specific ID</returns>
        [HttpGet("user/{userId}")]
        public async Task<IEnumerable<OrderGetDto>> GetOrdersOfUsers(int userId)
        {
            _logger.LogInformation("Getting orders of a user");
            return await _orderService.GetOrdersOfUser(userId);
        }



        /// <summary>
        /// Creates a new Order with provided order details
        /// </summary>
        /// <param name="orderAddDto">The data transfer object containing the create order information</param>
        /// <returns>A task representing the asynchronous operation</returns>
        [HttpPost]
        public async Task CreateOrder(OrderAddDto orderAddDto)
        {
           
            var order = new Order
            {
                UserId = orderAddDto.UserId,
                OrderItems = orderAddDto.OrderItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };

            order.OrderAmount = order.OrderItems.Sum(item => item.Quantity * item.UnitPrice);

            _logger.LogInformation("Creating a new Order");
            await _orderService.CreateOrder(order);
        }
    }
}
