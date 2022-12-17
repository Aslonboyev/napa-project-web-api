using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NapaProject.Services.ViewModels.Users
{
    public class UserCreateViewModel
    {
        [Required, MinLength(3)]
        [JsonPropertyName("username")]
        [FromForm(Name = "username")]
        public string Username { get; set; } = String.Empty;

        [Required(ErrorMessage = "Password is required"), MaxLength(50), MinLength(8)]
        [JsonPropertyName("password")]
        [FromForm(Name = "password")]
        public string Password { get; set; } = String.Empty;
    }
}
