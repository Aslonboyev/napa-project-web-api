using System.Text.Json.Serialization;

namespace NapaProject.Services.ViewModels.Users
{
    public class UserViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; } = String.Empty;
    }
}