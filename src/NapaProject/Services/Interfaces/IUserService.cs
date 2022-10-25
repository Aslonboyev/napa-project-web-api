using NapaProject.Models;
using NapaProject.Services.ViewModels.Users;
using System.Linq.Expressions;

namespace NapaProject.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetAsync(Expression<Func<User, bool>> expression);

        Task DeleteAsync(Expression<Func<User, bool>> expression);

        Task DeleteAsync();

        Task<UserViewModel> UpdateAsync(UserPatchViewModel model);

        Task<IEnumerable<UserViewModel>> GetAllAsync(Expression<Func<User, bool>>? expression = null);
    }
}