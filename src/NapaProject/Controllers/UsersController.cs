using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NapaProject.Services.Interfaces;
using NapaProject.Services.ViewModels.Users;

namespace NapaProject.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPatch(), Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateAsync([FromForm] UserPatchViewModel userCreateViewModel)
        {
            return Ok(await _service.UpdateAsync(userCreateViewModel));
        }

        [HttpGet("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok(await _service.GetAsync(p => p.Id == id));
        }

        [HttpDelete(), Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteAsync()
        {
            await _service.DeleteAsync();
            return Ok();
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            await _service.DeleteAsync(p => p.Id == id);
            return Ok();
        }
    }
}