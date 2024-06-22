using EStore.Application.Services.Abstracts;
using EStore.Domain.DTO_s;
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

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Admin,Cashier")]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequestDTO addCategoryRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _categoryService.AddCategoryAsync(addCategoryRequestDTO);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin,Cashier")]

        public async Task<IActionResult> GetAllCategories([FromQuery] GetAllCategoryRequestDTO getAllCategoryRequestDTO)
        {
            var categories = await _categoryService.GetAllCategoriesAsync(getAllCategoryRequestDTO);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,Cashier")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,Cashier")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryRequestDTO updateCategoryRequestDTO)
        {
            try
            {
                if (id != updateCategoryRequestDTO.Id)
                {
                    return BadRequest(new { Message = "Mismatch between route id and body id." });
                }

                var response = await _categoryService.UpdateCategoryAsync(updateCategoryRequestDTO);
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return NoContent();
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(response.Message);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, new { Message = response.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,Cashier")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var response = await _categoryService.DeleteCategoryAsync(id);
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return NoContent();
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(response.Message);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, new { Message = response.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }



    }
}
