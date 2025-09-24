using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary1.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)] // adjust length as needed
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Range(0.0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        // Foreign key
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }


        [Url]
        public string? ImageUrl { get; set; }

        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}