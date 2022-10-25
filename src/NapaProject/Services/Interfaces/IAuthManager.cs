using NapaProject.Models;

namespace NapaProject.Services.Interfaces
{
    public interface IAuthManager
    {
        public string GenerateToken(User user);
    }
}
