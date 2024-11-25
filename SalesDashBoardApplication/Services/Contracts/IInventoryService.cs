using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Models.DTO.InventoryDto;

namespace SalesDashBoardApplication.Services.Contracts
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryGetDto>> GetAllInventoryItems();
        Task<IEnumerable<InventoryGetDto>> GetAllLowStockItems();
        Task<InventoryGetDto> GetInventoryByProductId(int productId);
        Task ReorderInventory(int productId);
    }
}
