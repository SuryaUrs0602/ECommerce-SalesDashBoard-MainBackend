using Microsoft.EntityFrameworkCore;
using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Repositories.Contracts;

namespace SalesDashBoardApplication.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SalesDashBoardDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(SalesDashBoardDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var inventory = new Inventory
            {
                StockLevel = 30,
                ReorderLevel = 10,
                ProductId = product.ProductId   
            };

            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Added a new product {product}");
        }

        public async Task DeleteProduct(int id)
        {
            var product = await GetProductById(id);
            if (product !=  null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Deleted the Product with ID {id}");
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            _logger.LogInformation("Fetching all Products Data");
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            _logger.LogInformation($"Fetching Product with ID {id}");
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            _logger.LogInformation($"Fetching all the products of category {category}");
            return await _context.Products
                .Where(p => p.ProductCategory == category)
                .ToListAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Updated Product {product}");
        }
    }
}
