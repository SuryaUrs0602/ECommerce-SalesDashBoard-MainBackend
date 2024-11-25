using Microsoft.EntityFrameworkCore;
using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Repositories.Contracts;

namespace SalesDashBoardApplication.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SalesDashBoardDbContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(SalesDashBoardDbContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateOrder(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {
                var product = await _context.Products
                    .Include(inv => inv.Inventory)
                    .FirstOrDefaultAsync(p => p.ProductId == orderItem.ProductId);

                if (product == null)
                    throw new ApplicationException($"Product with ID {orderItem.ProductId} not found.");

                if (product.Inventory.StockLevel < orderItem.Quantity)
                    throw new ApplicationException($"Insufficient stock for product {product.ProductName}. Available: {product.Inventory.StockLevel}, Required: {orderItem.Quantity}.");

                product.Inventory.StockLevel -= orderItem.Quantity;
            }
            _context.Add(order);
            await _context.SaveChangesAsync();

            await UpdateSalesPerformance(order);

            await UpdateRevenue(order);

            _logger.LogInformation($"Added a new Order {order}");
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            _logger.LogInformation("Fetching all Order details");
            return await _context.Orders
                .Include(user => user.User)
                .Include(ord => ord.OrderItems)
                .ThenInclude(pro => pro.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userId)
        {
            _logger.LogInformation("Fetching all ordres of a customer");
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(user => user.User)
                .Include(ord => ord.OrderItems)
                .ThenInclude(pro => pro.Product)
                .ToListAsync();
        }





        private async Task UpdateSalesPerformance(Order order)
        {
            var orderDate = order.OrderDate.Date;

            var salesPerformace = await _context.SalesPerformances
                .FirstOrDefaultAsync(sp => sp.Date.Date == orderDate);

            if (salesPerformace == null)
            {
                salesPerformace = new SalesPerformance { Date = orderDate };
                _context.SalesPerformances.Add(salesPerformace);
                _logger.LogInformation($"Creating new SalesPerformance record for {orderDate}");
            }
            else
                _logger.LogInformation($"Updating existing SalesPerformance record for {orderDate}");

            salesPerformace.TotalOrders++;

            var totalAmount = await _context.Orders
                .Where(o => o.OrderDate.Date == orderDate)
                .SumAsync(o => o.OrderAmount);

            salesPerformace.AverageOrderValue = totalAmount / salesPerformace.TotalOrders;

            var orderedUserIds = await _context.Orders
                .Where(o => o.OrderDate.Date == orderDate)
                .Select(o => o.UserId)
                .Distinct()
                .ToListAsync();

            if (!orderedUserIds.Contains(order.UserId))
            {
                orderedUserIds.Add(order.UserId);
            }
            salesPerformace.CountOfOrderedUser = orderedUserIds.Count;

            salesPerformace.CountOfusers = await _context.Users.CountAsync();

            var unitsSold = await _context.OrderItems
                .Where(oi => oi.Order.OrderDate.Date == orderDate)
                .SumAsync(oi => oi.Quantity);
            salesPerformace.CountOfUnitSold = unitsSold;

            var mostOrderedProduct = await _context.OrderItems
                .Where(oi => oi.Order.OrderDate.Date == orderDate)
                .GroupBy(oi => oi.ProductId)
                .OrderByDescending(g => g.Sum(oi => oi.Quantity))
                .Select(g => new { ProductId = g.Key, Quantity = g.Sum(oi => oi.Quantity) })
                .FirstOrDefaultAsync();

            if (mostOrderedProduct != null)
            {
                var product = await _context.Products.FindAsync(mostOrderedProduct.ProductId);
                salesPerformace.MostOrderedProduct = product?.ProductName ?? string.Empty;
            }
            else
            {
                var currentMostOrdered = order.OrderItems?
                    .GroupBy(oi => oi.ProductId)
                    .OrderByDescending(g => g.Sum(oi => oi.Quantity))
                    .FirstOrDefault();
                if (currentMostOrdered != null)
                {
                    var product = await _context.Products.FindAsync(currentMostOrdered.Key);
                    salesPerformace.MostOrderedProduct = product?.ProductName ?? string.Empty;
                }
                else
                {
                    salesPerformace.MostOrderedProduct = string.Empty;
                }
            }

            var previousDayDate = orderDate.AddDays(-1);
            var previousDayPerformance = await _context.SalesPerformances
                .Where(sp => sp.Date == previousDayDate)
                .Select(sp => sp.TotalOrders)
                .FirstOrDefaultAsync();

            salesPerformace.SalesGrowthRate = previousDayPerformance > 0
                ? ((salesPerformace.TotalOrders - previousDayPerformance) / (double)previousDayPerformance) * 100
                : 100;

            await _context.SaveChangesAsync();

            _logger.LogInformation($"SalesPerformance updated for {orderDate}. TotalOrders: {salesPerformace.TotalOrders}, CountOfOrderedUser: {salesPerformace.CountOfOrderedUser}");
        }


        private async Task UpdateRevenue(Order order)
        {
            var orderDate = order.OrderDate.Date;

            var revenue = await _context.Revenues
                .FirstOrDefaultAsync(sp => sp.Date.Date == orderDate);

            if (revenue == null)
            {
                revenue = new Revenue { Date = orderDate };
                _context.Revenues.Add(revenue);
                _logger.LogInformation($"creating a new revenue record for {orderDate}");
            }

            else
                _logger.LogInformation($"Updating existing Revenue record for {orderDate}");


            revenue.TotalRevenue = await _context.Orders
                .Where(sp => sp.OrderDate.Date == orderDate)
                .SumAsync(o => o.OrderAmount);

            var totalOrders = await _context.Orders
                .CountAsync(sp => sp.OrderDate.Date == orderDate);

            revenue.AverageRevenuePerOrder = totalOrders > 0 ? revenue.TotalRevenue / totalOrders : 0;


            revenue.TotalCost = await _context.Products
                .Join(_context.Inventories,
                 p => p.ProductId,
                 i => i.ProductId,
                 (p, i) => i.StockLevel * p.ProductPrice)
                .SumAsync();


            revenue.AverageCostPerOrder = totalOrders > 0 ? revenue.TotalCost / totalOrders : 0;

            var previousDayDate = orderDate.AddDays(-1);
            var previousDayRevenue = await _context.Revenues
                .Where(r => r.Date == previousDayDate)
                .Select(r => r.TotalRevenue)
                .FirstOrDefaultAsync();

            revenue.RevenueGrowthRate = previousDayRevenue > 0
                ? ((revenue.TotalRevenue - previousDayRevenue) / previousDayRevenue) * 100
                : 100;

            await _context.SaveChangesAsync();

            _logger.LogInformation($"Revenue updated for {orderDate}. TotalRevenue: {revenue.TotalRevenue}, TotalCost: {revenue.TotalCost}");

        }
    }
}
