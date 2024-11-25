using Microsoft.EntityFrameworkCore;
using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Repositories.Contracts;

namespace SalesDashBoardApplication.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly SalesDashBoardDbContext _context;
        private readonly ILogger<InventoryRepository> _logger;

        public InventoryRepository(SalesDashBoardDbContext context, ILogger<InventoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Inventory>> GetAllInventoryData()
        {
            _logger.LogInformation("Fetching all Inventory Details");
            return await _context.Inventories
                .Include(pro => pro.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<Inventory>> GetAllInventoryOfLowStock()
        {
            _logger.LogInformation("Fetching all Inventory Details having low-stock");
            return await _context.Inventories
                .Include(pro => pro.Product)
                .Where(i => i.StockLevel < 9)
                .ToListAsync();
        }

        public async Task<Inventory> GetInventoryByProductId(int productId)
        {
            _logger.LogInformation($"Fetching Inventory Details of a product id {productId}");
            return await _context.Inventories
                .Include(pro => pro.Product)
                .FirstAsync(i => i.ProductId == productId);
        }

        public async Task ReorderInventory(int productId)
        {
            _logger.LogInformation("Reordering Inventory Stock Level");
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.ProductId == productId);
            if (inventory != null && inventory.StockLevel <= inventory.ReorderLevel)
            {

                var totalQuantitySold = await _context.OrderItems
                    .Where(product => product.ProductId == productId)
                    .SumAsync(quantity => quantity.Quantity);

                double reorderAmount = totalQuantitySold + (totalQuantitySold * 0.6);

                inventory.StockLevel += (int)Math.Ceiling(reorderAmount);

                _context.Inventories.Update(inventory);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Reordered the Stock Level of the product with productID {productId}");
            }
        }
    }
}
