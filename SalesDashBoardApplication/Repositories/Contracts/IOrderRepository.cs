using SalesDashBoardApplication.Models;

namespace SalesDashBoardApplication.Repositories.Contracts
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task CreateOrder(Order order);
        Task<IEnumerable<Order>> GetOrdersByUserId(int userId);
    }
}
