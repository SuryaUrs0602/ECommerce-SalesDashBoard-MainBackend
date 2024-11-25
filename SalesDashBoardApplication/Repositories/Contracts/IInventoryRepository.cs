using SalesDashBoardApplication.Models;

namespace SalesDashBoardApplication.Repositories.Contracts
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAllInventoryData();
        Task<IEnumerable<Inventory>> GetAllInventoryOfLowStock();
        Task<Inventory> GetInventoryByProductId(int productId);
        Task ReorderInventory(int productId);
    }
}
