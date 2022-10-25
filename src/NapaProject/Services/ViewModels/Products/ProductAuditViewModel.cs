using System.Text.Json.Serialization;

namespace NapaProject.Services.ViewModels.Products
{
    public class ProductAuditViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("changed_by")]
        public string ChangedBy { get; set; } = String.Empty;

        [JsonPropertyName("product_id")]
        public long ProductId { get; set; }

        [JsonPropertyName("changed_time")]
        public DateTime ChangedTime { get; set; }

        [JsonPropertyName("changed")]
        public string Changed { get; set; } = String.Empty;
    }
}
