using NapaProject.Services.ViewModels.Users;

namespace NapaProject.Services.Interfaces
{
    public interface IAccountService
    {
        Task RegisterAsync(UserCreateViewModel model);

        Task<string?> LogInAsync(UserCreateViewModel model);
    }
}