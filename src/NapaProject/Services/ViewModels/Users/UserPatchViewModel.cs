using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NapaProject.Services.ViewModels.Users
{
    public class UserPatchViewModel
    {
        [MinLength(3)]
        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [MaxLength(50), MinLength(8)]
        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}
