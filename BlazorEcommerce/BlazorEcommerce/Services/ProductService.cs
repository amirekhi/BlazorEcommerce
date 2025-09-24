using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorEcommerce.Data;
using BlazorEcommerce.DTOs.Entities;
using Classlibrary1.Services;
using ClassLibrary1.Entities;
using Microsoft.EntityFrameworkCore;


namespace ClassLibrary1.Services
{
    public class ProductService : IProductService
    {
        private DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProduct(ProductDTO productDTO)
        {
            var product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                CategoryId = productDTO.CategoryId,
                ImageUrl = productDTO.ImageUrl,
                CreatedAt = DateTime.UtcNow
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }



        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Product?> EditProduct(int id, ProductDTO newProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            product.Name = newProduct.Name;
            product.Price = newProduct.Price;
            product.Description = newProduct.Description;
            product.ImageUrl = newProduct.ImageUrl;
            product.Stock = newProduct.Stock;
            product.CategoryId = newProduct.CategoryId;


            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }


        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }


        public async Task<IEnumerable<Product>> SearchProductsAsync(string query)
        {
            var lowerQuery = $"%{query.ToLower()}%";

            return await _context.Products
                .Where(p =>
                    EF.Functions.Like(p.Name.ToLower(), lowerQuery) ||
                    EF.Functions.Like(p.Description.ToLower(), lowerQuery))
                .ToListAsync();
        }




    }
}