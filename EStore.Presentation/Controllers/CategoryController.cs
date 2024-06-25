using Azure;
using EStore.Application.Repositories.Concretes;
using EStore.Application.Services.Abstracts;
using EStore.Domain.DTO_s;
using EStore.Domain.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryService categoryService, ICategoryRepository categoryRepository)
        {
            _categoryService = categoryService;
            _categoryRepository = categoryRepository;
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryVM categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _categoryService.AddCategoryAsync(categoryVM);
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCategories(int page,int size)
        {
            var categories = await _categoryService.GetAllCategoriesAsync(page,size);
            return Ok(categories);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);

        }

        [HttpPut("[action]")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryVM updateCategoryVM)
        {
            try
            {
                if (id != updateCategoryVM.Id)
                {
                    return BadRequest(new { Message = "Mismatch between route id and body id." });
                }

                var response = await _categoryService.UpdateCategoryAsync(updateCategoryVM);
                if (response == HttpStatusCode.NoContent)
                {
                    return NoContent();
                }
                else if (response == HttpStatusCode.NotFound)
                {
                    return NotFound(response);
                }
                else
                {
                    return StatusCode((int)response, new { Message = response });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }

        }

        [HttpDelete("[action]/{id}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok();
        }
    }
}
