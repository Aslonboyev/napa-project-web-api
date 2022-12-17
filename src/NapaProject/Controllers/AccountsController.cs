using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NapaProject.Services.Interfaces;
using NapaProject.Services.ViewModels.Users;

namespace NapaProject.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _acountService;

        public AccountsController(IAccountService accountService)
        {
            _acountService = accountService;
        }

        [HttpPost("register"), AllowAnonymous]
        public async Task<IActionResult> Registr([FromForm] UserCreateViewModel userCreateViewModel)
        {
            await _acountService.RegisterAsync(userCreateViewModel);
            return Ok();
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> LogIn([FromForm] UserCreateViewModel logInViewModel)
            => Ok((await _acountService.LogInAsync(logInViewModel)));
    }
}
