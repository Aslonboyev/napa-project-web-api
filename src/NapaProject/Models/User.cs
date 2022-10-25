
using NapaProject.Enums;

namespace NapaProject.Models
{
    public class User
    {
        public long Id { get; set; }

        public string Username { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;

        public string Salt { get; set; } = String.Empty;

        public Role Role { get; set; } = Role.User;
    }
}
