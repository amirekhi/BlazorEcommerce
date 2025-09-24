using Classlibrary1.Services;
using BlazorEcommerce.Data;
using ClassLibrary1.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorEcommerce.DTOs.Entities;
using BlazorEcommerce.Extention;

namespace BlazorEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {

            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> AddProduct(ProductDTO product)
        {


            Product addedgame = await _productService.AddProduct(product);
            return Ok(addedgame.ToProductDTO());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(int id, ProductDTO productDTO)
        {
            Product updatedProduct = await _productService.EditProduct(id, productDTO);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return Ok(updatedProduct.ToProductDTO());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO?>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        public async Task<List<ProductDTO>> GetProducts()
        {
            return (await _productService.GetProducts()).Select(p => p.ToProductDTO()).ToList();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query cannot be empty.");

            var results = await _productService.SearchProductsAsync(query);
            return Ok(results);
        }

    }
}
