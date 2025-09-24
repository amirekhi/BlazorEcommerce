using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorEcommerce.DTOs.Entities;

namespace ClassLibrary1.DTOs.Entities
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; } = String.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
         
        public List<ProductDTO> Products { get; set; } = new(); // Only lightweight ProductDTOs
    }
}