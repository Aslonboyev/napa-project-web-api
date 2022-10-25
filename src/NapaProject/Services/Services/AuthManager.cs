using Microsoft.IdentityModel.Tokens;
using NapaProject.Models;
using NapaProject.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NapaProject.Services.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly IConfiguration _config;

        public AuthManager(IConfiguration configuration)
        {
            _config = configuration.GetSection("JWT");
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(_config["Issuer"], _config["Audience"], claims,
                expires: DateTime.Now.AddDays(double.Parse(_config["Lifetime"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
