using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NapaProject.Services.Interfaces;

namespace NapaProject.Controllers
{
    [Route("api/product-audit")]
    [ApiController]
    public class ProductAuditsController : ControllerBase
    {
        private readonly IProductAuditService _service;

        public ProductAuditsController(IProductAuditService service)
        {
            _service = service;
        }

        [HttpGet("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok(await _service.GetAsync(p => p.Id == id));
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }
    }
}
