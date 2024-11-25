using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Models.DTO.ProductDto;

namespace SalesDashBoardApplication.Services.Contracts
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        Task DeleteProduct(int id);
        Task UpdateProduct(Product product);            
        Task<ProductGetDto> GetProductById(int id);
        Task<IEnumerable<ProductGetDto>> GetAllProducts();
        Task<IEnumerable<ProductGetDto>> GetProductsByCategory(string category);
        Task<Product> FindProduct(int id);
    }
}
