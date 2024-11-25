using Microsoft.EntityFrameworkCore;
using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Models.DTO.ProductDto;
using SalesDashBoardApplication.Repositories.Contracts;
using SalesDashBoardApplication.Services.Contracts;

namespace SalesDashBoardApplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task AddProduct(Product product)
        {
            try
            {
                _logger.LogInformation("Attempting to add a new product");
                await _productRepository.AddProduct(product);
            }

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "A database update error occurred while adding a product.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while adding a product.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding a product.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to delete the product");
                await _productRepository.DeleteProduct(id);
            }

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "A database update error occurred while deleting a product.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while deleting a product.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting a product.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }

        public async Task<Product> FindProduct(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to find the particular Product with ID {id}");
                return await _productRepository.GetProductById(id);
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while finding the product with ID {Id}.", id);
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "A database update error occurred while finding the product with ID {Id}.", id);
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while finding the product with ID {Id}.", id);
                throw new ApplicationException("An error occurred while retrieving the product. Please try again later.");
            }
        }

        public async Task<IEnumerable<ProductGetDto>> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Attempting to get all products data");
                var products = await _productRepository.GetAllProducts();
                return products.Select(x => new ProductGetDto
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    ProductImageUrl = x.ProductImageUrl,
                    ProductDescription = x.ProductDescription,
                    ProductCategory = x.ProductCategory,
                    ProductPrice = x.ProductPrice
                });
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching all products");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching all products.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all products.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }

        }

        public async Task<ProductGetDto> GetProductById(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to get a particular product");
                var product = await _productRepository.GetProductById(id);

                return new ProductGetDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductImageUrl = product.ProductImageUrl,
                    ProductDescription = product.ProductDescription,
                    ProductCategory = product.ProductCategory,
                    ProductPrice = product.ProductPrice
                };
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching particular product");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching particular product.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching particular product.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }

        }

        public async Task<IEnumerable<ProductGetDto>> GetProductsByCategory(string category)
        {
            try
            {
                _logger.LogInformation("Attempting to get all products of a category");
                var products = await _productRepository.GetProductsByCategory(category);
                return products.Select(x => new ProductGetDto
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    ProductImageUrl = x.ProductImageUrl,
                    ProductDescription = x.ProductDescription,
                    ProductCategory = x.ProductCategory,
                    ProductPrice = x.ProductPrice
                });
            }

            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "An argument null exception occurred while fetching all products of a category");
                throw new ApplicationException("An error occurred: a required argument was missing.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while fetching all products of a category.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all products of a category.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }

        }

        public async Task UpdateProduct(Product product)
        {
            try
            {
                _logger.LogInformation("Attempting to update a particular product");
                await _productRepository.UpdateProduct(product);
            }

            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "A database update error occurred while updating a product.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (OperationCanceledException opEx)
            {
                _logger.LogError(opEx, "A operation canceled error occurred while updating a product.");
                throw new ApplicationException("An error occurred while accessing the database. Please try again later.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating a product.");
                throw new ApplicationException("An error occurred while adding the product. Please try again later.");
            }
        }
    }
}
