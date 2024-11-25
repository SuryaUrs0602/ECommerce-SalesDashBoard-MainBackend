using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Models.DTO.OrderDto;
using SalesDashBoardApplication.Repositories.Contracts;
using SalesDashBoardApplication.Services.Contracts;

namespace SalesDashBoardApplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderService> _logger;
        private readonly SalesDashBoardDbContext _context;
        private readonly MqttService _mqttService;

        private readonly IServiceProvider _serviceProvider;

        public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger, SalesDashBoardDbContext context, MqttService mqttService, IServiceProvider serviceProvider)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _context = context;
            _mqttService = mqttService;
            _serviceProvider = serviceProvider;
        }

        public async Task CreateOrder(Order order)
        {
            try
            {
                _logger.LogInformation("Attempting to create a new Order");

                //foreach (var orderItem in order.OrderItems)
                //{
                //    var product = await _context.Products
                //        .Include(inv => inv.Inventory)
                //        .FirstOrDefaultAsync(p => p.ProductId == orderItem.ProductId);

                //    if (product == null)
                //        throw new ApplicationException($"Product with ID {orderItem.ProductId} not found.");

                //    if (product.Inventory.StockLevel < orderItem.Quantity)
                //        throw new ApplicationException($"Insufficient stock for product {product.ProductName}. Available: {product.Inventory.StockLevel}, Required: {orderItem.Quantity}.");

                //    product.Inventory.StockLevel -= orderItem.Quantity;
                //}
                await _orderRepository.CreateOrder(order);      

                //await UpdateSalesPerformance(order);

                //await UpdateRevenue(order);



                await _mqttService.PublishUpdateAsync("ecommerce/new-order", new
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    UserId = order.UserId
                });

                await _mqttService.PublishUpdateAsync("ecommerce/revenue-update", new
                {
                    OrderDate = order.OrderDate,
                    OrderAmount = order.OrderAmount
                });

                await _mqttService.PublishUpdateAsync("ecommerce/sales-update", new
                {
                    OrderId = order.OrderId,
                    TotalAmount = order.OrderAmount
                });

                _logger.LogInformation("Order Created and Notification sent");
            }                           

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "A database update error occurred while creating an order.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while creating an order.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating an order.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<OrderGetDto>> GetAllOrders()
        {
            try
            {
                _logger.LogInformation("Attempting to fetch all orders data");
                var orders = await _orderRepository.GetAllOrders();
                return orders.Select(x => new OrderGetDto
                {
                    OrderId = x.OrderId,
                    OrderDate = x.OrderDate,
                    OrderAmount = x.OrderAmount,
                    TransactionReference = x.TransactionReference,
                    UserName = x.User.UserName,
                    UserEmail = x.User.UserEmail,
                    OrderItems = x.OrderItems.Select(o => new OrderItemGetDto
                    {
                        OrderItemId = o.OrderItemId,
                        ProductId = o.ProductId ?? 0,
                        Quantity = o.Quantity,
                        UnitPrice = o.UnitPrice,
                        ProductName = o.Product.ProductName,
                        ProductCategory = o.Product.ProductCategory,
                    }).ToList()
                });
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching all orders");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching all orders.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all orders.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<OrderGetDto>> GetOrdersOfUser(int userId)
        {
            try
            {
                _logger.LogInformation("Attempting to fetch all orders of a user");
                var orders = await _orderRepository.GetOrdersByUserId(userId);
                return orders.Select(x => new OrderGetDto
                {
                    OrderId = x.OrderId,
                    OrderDate = x.OrderDate,
                    OrderAmount = x.OrderAmount,
                    TransactionReference = x.TransactionReference,
                    UserName = x.User?.UserName ?? "UnKnown",
                    UserEmail = x.User?.UserEmail ?? "UnKnown",
                    OrderItems = x.OrderItems.Select(o => new OrderItemGetDto
                    {
                        OrderItemId = o.OrderItemId,
                        ProductId = o.ProductId ?? 0,
                        Quantity = o.Quantity,
                        UnitPrice = o.UnitPrice,
                        ProductName = o.Product?.ProductName ?? "UnKnown",
                        ProductCategory = o.Product?.ProductCategory ?? "UnKnown",
                    }).ToList()
                });
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching all orders of customer");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching all orders of customer.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all orders of customer.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }


        //private async Task UpdateSalesPerformance(Order order)
        //{
        //    var orderDate = order.OrderDate.Date;

        //    var salesPerformace = await _context.SalesPerformances
        //        .FirstOrDefaultAsync(sp => sp.Date.Date == orderDate);

        //    if (salesPerformace == null)
        //    {
        //        salesPerformace = new SalesPerformance { Date = orderDate };
        //        _context.SalesPerformances.Add(salesPerformace);
        //        _logger.LogInformation($"Creating new SalesPerformance record for {orderDate}");
        //    }
        //    else
        //        _logger.LogInformation($"Updating existing SalesPerformance record for {orderDate}");

        //    salesPerformace.TotalOrders++;

        //    var totalAmount = await _context.Orders
        //        .Where(o => o.OrderDate.Date == orderDate)
        //        .SumAsync(o => o.OrderAmount);

        //    salesPerformace.AverageOrderValue = totalAmount / salesPerformace.TotalOrders;

        //    var orderedUserIds = await _context.Orders
        //        .Where(o => o.OrderDate.Date == orderDate)
        //        .Select(o => o.UserId)
        //        .Distinct()
        //        .ToListAsync();

        //    if (!orderedUserIds.Contains(order.UserId))
        //    {
        //        orderedUserIds.Add(order.UserId);
        //    }
        //    salesPerformace.CountOfOrderedUser = orderedUserIds.Count;

        //    salesPerformace.CountOfusers = await _context.Users.CountAsync();

        //    var unitsSold = await _context.OrderItems
        //        .Where(oi => oi.Order.OrderDate.Date == orderDate)
        //        .SumAsync(oi => oi.Quantity);
        //    salesPerformace.CountOfUnitSold = unitsSold;

        //    var mostOrderedProduct = await _context.OrderItems
        //        .Where(oi => oi.Order.OrderDate.Date == orderDate)
        //        .GroupBy(oi => oi.ProductId)
        //        .OrderByDescending(g => g.Sum(oi => oi.Quantity))
        //        .Select(g => new { ProductId = g.Key, Quantity = g.Sum(oi => oi.Quantity) })
        //        .FirstOrDefaultAsync();

        //    if (mostOrderedProduct != null)
        //    {
        //        var product = await _context.Products.FindAsync(mostOrderedProduct.ProductId);
        //        salesPerformace.MostOrderedProduct = product?.ProductName ?? string.Empty;
        //    }
        //    else
        //    {
        //        var currentMostOrdered = order.OrderItems?
        //            .GroupBy(oi => oi.ProductId)
        //            .OrderByDescending(g => g.Sum(oi => oi.Quantity))
        //            .FirstOrDefault();
        //        if (currentMostOrdered != null)
        //        {
        //            var product = await _context.Products.FindAsync(currentMostOrdered.Key);
        //            salesPerformace.MostOrderedProduct = product?.ProductName ?? string.Empty;
        //        }
        //        else
        //        {
        //            salesPerformace.MostOrderedProduct = string.Empty;
        //        }
        //    }

        //    var previousDayDate = orderDate.AddDays(-1);
        //    var previousDayPerformance = await _context.SalesPerformances
        //        .Where(sp => sp.Date == previousDayDate)
        //        .Select(sp => sp.TotalOrders)
        //        .FirstOrDefaultAsync();

        //    salesPerformace.SalesGrowthRate = previousDayPerformance > 0
        //        ? ((salesPerformace.TotalOrders - previousDayPerformance) / (double)previousDayPerformance) * 100
        //        : 100; 

        //    await _context.SaveChangesAsync();

        //    _logger.LogInformation($"SalesPerformance updated for {orderDate}. TotalOrders: {salesPerformace.TotalOrders}, CountOfOrderedUser: {salesPerformace.CountOfOrderedUser}");
        //}


        //private async Task UpdateRevenue(Order order)
        //{
        //    var orderDate = order.OrderDate.Date;

        //    var revenue = await _context.Revenues
        //        .FirstOrDefaultAsync(sp => sp.Date.Date == orderDate);

        //    if (revenue == null)
        //    {
        //        revenue = new Revenue { Date = orderDate };
        //        _context.Revenues.Add(revenue);
        //        _logger.LogInformation($"creating a new revenue record for {orderDate}");
        //    }

        //    else
        //        _logger.LogInformation($"Updating existing Revenue record for {orderDate}");


        //    revenue.TotalRevenue = await _context.Orders
        //        .Where(sp => sp.OrderDate.Date == orderDate)
        //        .SumAsync(o => o.OrderAmount);

        //    var totalOrders = await _context.Orders
        //        .CountAsync(sp => sp.OrderDate.Date == orderDate);

        //    revenue.AverageRevenuePerOrder = totalOrders > 0 ? revenue.TotalRevenue / totalOrders : 0;


        //    revenue.TotalCost = await _context.Products
        //        .Join(_context.Inventories,
        //         p => p.ProductId,
        //         i => i.ProductId,
        //         (p, i) => i.StockLevel * p.ProductPrice)
        //        .SumAsync();


        //    revenue.AverageCostPerOrder = totalOrders > 0 ? revenue.TotalCost / totalOrders : 0;

        //    var previousDayDate = orderDate.AddDays(-1);
        //    var previousDayRevenue = await _context.Revenues
        //        .Where(r => r.Date == previousDayDate)
        //        .Select(r => r.TotalRevenue)
        //        .FirstOrDefaultAsync();

        //    revenue.RevenueGrowthRate = previousDayRevenue > 0
        //        ? ((revenue.TotalRevenue - previousDayRevenue) / previousDayRevenue) * 100
        //        : 100; 

        //    await _context.SaveChangesAsync();

        //    _logger.LogInformation($"Revenue updated for {orderDate}. TotalRevenue: {revenue.TotalRevenue}, TotalCost: {revenue.TotalCost}");

        //}

    }
}
