using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Entities;
using ClassLibrary1.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace BlazorEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            return categories;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            CategoryDTO category = await _categoryService.GetByIdAsync(id);
            return category is null ? NotFound() : Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Create(CategoryDTO categoryDto)
        {
            var createdCategory = await _categoryService.CreateAsync(categoryDto);

            if (createdCategory == null)
            {
                return StatusCode(500, "An error occurred while creating the category.");
            }

            return Ok(createdCategory);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDTO>> Update(CategoryDTO categoryDto, int id)
        {
            var updated = await _categoryService.UpdateAsync(categoryDto, id);
            return updated is null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            await _categoryService.DeleteAsync(id) ? Ok() : NotFound();
    }

    // Removed unnecessary IActionResult<T> interface
}