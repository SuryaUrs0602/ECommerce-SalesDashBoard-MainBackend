using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesDashBoardApplication.Models.DTO.InventoryDto;
using SalesDashBoardApplication.Services.Contracts;

namespace SalesDashBoardApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly ILogger<InventoriesController> _logger;

        public InventoriesController(IInventoryService inventoryService, ILogger<InventoriesController> logger)
        {
            _inventoryService = inventoryService;
            _logger = logger;
        }


        /// <summary>
        /// Retrives the list of all inventory data
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing an List of inventory data</returns>

        [HttpGet]
        public async Task<IEnumerable<InventoryGetDto>> AllInventoryData()
        {
            _logger.LogInformation("Getting all Inventory Details");
            return await _inventoryService.GetAllInventoryItems();
        }


        /// <summary>
        /// Retrives the list of all the products having low stock level
        /// </summary>
        /// <returns></returns>
        [HttpGet("low-stock")]
        public async Task<IEnumerable<InventoryGetDto>> LowStockInventoryData()
        {
            _logger.LogInformation("Getting all low stock inventory data");
            return await _inventoryService.GetAllLowStockItems();
        }



        /// <summary>
        /// Reorder/Increases the stock level of a product identified by their ID
        /// </summary>
        /// <param name="productId">The unique identifier of the product to reorder</param>
        /// <returns>A task representing the asynchronous operation</returns>
        [HttpPost("reorder/{productId}")]
        public async Task ReorderStock(int productId)
        {
            _logger.LogInformation("Re-ordering stock value");
            await _inventoryService.ReorderInventory(productId);
        }



        /// <summary>
        /// Retrieves the inventory data for a specific inventory identified by their ID.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to retrive</param>
        /// <returns>A task representing the asynchronous operation, containing the inventory details associated with the specific ID</returns>
        [HttpGet("{productId}")]
        public async Task<InventoryGetDto> InventoryByProductId(int productId)
        {
            _logger.LogInformation("Getting inventory by product id");
            return await _inventoryService.GetInventoryByProductId(productId);
        }

    }
}
