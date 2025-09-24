using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEcommerce.DTOs.Entities
{
  public class ProductDTO
  {
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public int? CategoryId { get; set; }  // Just the foreign key ID, no Category object

    public string? ImageUrl { get; set; }

    public List<string> Images { get; set; } = new();

    public DateTime CreatedAt { get; set; }

  }
}
