using SalesDashBoardApplication.Models;

namespace SalesDashBoardApplication.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
        Task<IEnumerable<Product>> GetProductsByCategory(string category);

    }
}
