using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Entities;

namespace ClassLibrary1.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO?> GetByIdAsync(int id);
        Task<CategoryDTO?> CreateAsync(CategoryDTO category);
        Task<CategoryDTO?> UpdateAsync(CategoryDTO category , int id);
        Task<bool> DeleteAsync(int id);
    }
}