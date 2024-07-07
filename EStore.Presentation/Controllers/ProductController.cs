using EStore.Application.Services.Abstracts;
using EStore.Domain.DTO_s;
using EStore.Domain.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("AddProduct")]
        [Authorize(Roles ="SuperAdmin,Admin,Cashier")]
        public async Task<IActionResult> AddProduct([FromBody]AddProductVM addProductVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(addProductVM);
            }
            await _productService.AddProductAsync(addProductVM);
            return Ok();
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products=await _productService.GetAllProductsAsync();

            return Ok(products);
        }

        [HttpGet("GetByIdProduct")]
        [Authorize(Roles = "SuperAdmin,Admin,Cashier")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result=await _productService.GetProductByIdAsync(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("DeleteProduct")]
        [Authorize(Roles = "SuperAdmin,Admin,Cashier")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _productService.DeleteProductAsync(id);
            return Ok();
        }
        [HttpPut("UpdateProduct")]
        [Authorize(Roles = "SuperAdmin,Admin,Cashier")]
        public async Task<IActionResult>UpdateProductAsync(UpdateProductVM updateProductVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _productService.UpdateProductAsync(updateProductVM);
            return Ok();
        }
    }
}
