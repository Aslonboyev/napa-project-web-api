using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NapaProject.Services.Interfaces;
using NapaProject.Services.ViewModels.Products;

namespace NapaProject.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync(ProductCreateViewModel model)
        {
            return Ok(await _service.CreateAsync(model));
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            await _service.DeleteAsync(p => p.Id == id);
            return Ok();
        }

        [HttpGet("{id}"), Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok(await _service.GetAsync(p => p.Id == id));
        }

        [HttpGet("{id}/admin"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminAsync(long id)
        {
            return Ok(await _service.GetForAdminAsync(p => p.Id == id));
        }

        [HttpPatch("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long id, ProductPatchViewModel model)
        {
            await _service.UpdateAsync(id, model);
            return Ok();
        }

        [HttpGet("admin"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllForAdminAsync()
        {
            return Ok(await _service.GetAllForAdminAsync());
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }
    }
}
