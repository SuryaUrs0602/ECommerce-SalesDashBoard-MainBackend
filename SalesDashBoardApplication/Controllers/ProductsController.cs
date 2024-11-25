using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SalesDashBoardApplication.Models;
using SalesDashBoardApplication.Models.DTO.ProductDto;
using SalesDashBoardApplication.Services.Contracts;

namespace SalesDashBoardApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }


        /// <summary>
        /// Creates a new product with the provided product details
        /// </summary>
        /// <param name="productDto">The data transfer object containing the create product details</param>
        /// <returns>A task representing the asynchronous operation</returns>
        [HttpPost]
        public async Task AddProduct(ProductDto productDto)
        {
            var product = new Product
            {
                ProductName = productDto.ProductName,
                ProductImageUrl = productDto.ProductImageUrl,
                ProductDescription = productDto.ProductDescription,
                ProductCategory = productDto.ProductCategory,
                ProductPrice = productDto.ProductPrice
            };

            _logger.LogInformation("Creating a new Product");
            await _productService.AddProduct(product);
        }


        /// <summary>
        /// Deletes the product with the provided product Id
        /// </summary>
        /// <param name="id">Unique identifier to delete the product</param>
        /// <returns>A task representing the asynchronous operation</returns>
        [HttpDelete("{id}")]
        public async Task DeleteProduct(int id)
        {
            _logger.LogInformation("Deleting the product");
            await _productService.DeleteProduct(id);
        }


        /// <summary>
        /// Retrives the details of all the products
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing an List of product data</returns>
        [HttpGet]
        public async Task<IEnumerable<ProductGetDto>> GetAllProducts()
        {
            _logger.LogInformation("Getting all Products data");
            return await _productService.GetAllProducts();
        }


        /// <summary>
        /// Retirves the detail s of a particular product with Id
        /// </summary>
        /// <param name="id">Unique identifier to retrive the product data</param>
        /// <returns>A task representing the asynchronous operation, containing product data</returns>
        [HttpGet("{id}")]
        public async Task<ProductGetDto> GetProductById(int id)
        {
            _logger.LogInformation("Getting product by ID");
            return await _productService.GetProductById(id);
        }



        /// <summary>
        /// Retrives the details of all the products belonging to a particular category
        /// </summary>
        /// <param name="category">Unique identifier to retrive all the product details</param>
        /// <returns>A task representing the asynchronous operation, containing an List of product data</returns>
        [HttpGet("category/{category}")]
        public async Task<IEnumerable<ProductGetDto>> GetProductsByCategory(string category)
        {
            _logger.LogInformation("Getting all products of a particular category");
            return await _productService.GetProductsByCategory(category);
        }



        /// <summary>
        /// Applies partial updates to a product's details identified by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the product to update</param>
        /// <param name="patchDocument">The JSON Patch document containing the updates to apply</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ApplicationException">Thrown when the patch document is null, the product cannot be found, or the update fails</exception>
        [HttpPatch("{id}")]
        public async Task UpdateProduct(int id, JsonPatchDocument<ProductDto> patchDocument)
        {
            if (patchDocument == null)
                throw new ApplicationException("Cannot Update Product Try after some time");

            var existingProduct = await _productService.FindProduct(id);

            if (existingProduct == null)
                throw new ApplicationException("Cannot Find Product Try after some time");

            var productDto = new ProductDto
            {
                ProductName = existingProduct.ProductName,
                ProductImageUrl = existingProduct.ProductImageUrl,
                ProductDescription = existingProduct.ProductDescription,
                ProductCategory = existingProduct.ProductCategory,
                ProductPrice = existingProduct.ProductPrice
            };

            patchDocument.ApplyTo(productDto, ModelState);

            if (!ModelState.IsValid)
                throw new ApplicationException("Cannot Update Product due some issue");

            existingProduct.ProductName = productDto.ProductName;
            existingProduct.ProductImageUrl = productDto.ProductImageUrl;
            existingProduct.ProductDescription = productDto.ProductDescription;
            existingProduct.ProductCategory = productDto.ProductCategory;
            existingProduct.ProductPrice = productDto.ProductPrice;

            _logger.LogInformation("Updating the Product");
            await _productService.UpdateProduct(existingProduct);
        }
    }
}
