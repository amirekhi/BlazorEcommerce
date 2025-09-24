using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorEcommerce.DTOs.Entities;
using ClassLibrary1.Entities;

namespace Classlibrary1.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();

        Task<Product?> GetProductById(int id);
        Task<Product> AddProduct(ProductDTO productDTO);

        Task<Product?> EditProduct(int id, ProductDTO newProduct);

        Task<IEnumerable<Product>> SearchProductsAsync(string query);

        Task<bool> DeleteProduct(int id);
    }
}