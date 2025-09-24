using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorEcommerce.DTOs.Entities;
using ClassLibrary1.Entities;

namespace BlazorEcommerce.Extention
{
    public static class ProductExtentions
    {
        public static ProductDTO ToProductDTO(this Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl,  
                Images = product.Images.Select(img => img.ImageUrl).ToList()           
            };
        }

        public static Product ToEntity(this ProductDTO productDTO)
        {
            return new Product
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                CategoryId = productDTO.CategoryId,
                ImageUrl = productDTO.ImageUrl,       
            };
        }
    }
}