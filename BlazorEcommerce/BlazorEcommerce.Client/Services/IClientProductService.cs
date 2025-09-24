using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorEcommerce.DTOs.Entities;


namespace BlazorEcommerce.Client.Services
{
    public interface IClientProductService
    {
        Task<List<ProductDTO>> GetProducts();

        Task<ProductDTO?> GetProductById(int id);
        Task<ProductDTO> AddProduct(ProductDTO productDTO);

        Task<ProductDTO?> EditProduct(int id, ProductDTO newProduct);

        Task<List<ProductDTO>> SearchProductsAsync(string query);

        Task<bool> DeleteProduct(int id);
    }
}