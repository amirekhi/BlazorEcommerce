using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorEcommerce.Data;
using BlazorEcommerce.Mappers;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Entities;
using ClassLibrary1.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Services
{
  public class NewCategoryService : ICategoryService
    {
        private readonly DataContext _context;

        public NewCategoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            return await _context.Categories.Include(c => c.Products)
                .Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    CreatedAt = c.CreatedAt,
                    Products = c.Products.Select(p => p.ToDTO()).ToList()
                })
                .ToListAsync();
        }


        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    CreatedAt = c.CreatedAt,
                    Products = c.Products.Select(p => p.ToDTO()).ToList()
                })
                .FirstOrDefaultAsync();
        }



       public async Task<CategoryDTO?> CreateAsync(CategoryDTO categoryDto)
            {
                try
                {
                    _context.Categories.Add(categoryDto.FromDTO());
                    await _context.SaveChangesAsync();
                    return categoryDto;
                }
                catch (DbUpdateException ex)
                {
                    // Log the error (optional)
                    Console.WriteLine($"Database update error: {ex.Message}");
                    return null;
                }
                catch (Exception ex)
                {
                    // Catch any other unexpected exceptions
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                    return null;
                }
            }

        public async Task<CategoryDTO?> UpdateAsync(CategoryDTO categoryDto , int id)
        {
            var existing = await _context.Categories.FindAsync(id);
            if (existing is null) return null;

            existing.Name = categoryDto.Name;
            existing.Description = categoryDto.Description;
            existing.CreatedAt = categoryDto.CreatedAt;
            await _context.SaveChangesAsync();
            return existing.ToDTO();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Categories.FindAsync(id);
            if (existing is null) return false;

            _context.Categories.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}