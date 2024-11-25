using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Models.DTO.OrderDto;

namespace SalesDashBoardApplication.Services.Contracts
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderGetDto>> GetAllOrders();
        Task<IEnumerable<OrderGetDto>> GetOrdersOfUser(int userId);
        Task CreateOrder(Order order);
    }
}
