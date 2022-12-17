using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace NapaProject.Services.ViewModels.Products
{
    public class ProductAuditViewModel
    {
        [JsonPropertyName("id")]
        [FromForm(Name = "id")]
        public long Id { get; set; }

        [JsonPropertyName("changed_by")]
        [FromForm(Name = "changed_by")]
        public string ChangedBy { get; set; } = String.Empty;

        [JsonPropertyName("product_id")]
        [FromForm(Name = "product_id")]
        public long ProductId { get; set; }

        [JsonPropertyName("changed_time")]
        [FromForm(Name = "changed_time")]
        public DateTime ChangedTime { get; set; }

        [JsonPropertyName("changed")]
        [FromForm(Name = "changed")]
        public string Changed { get; set; } = String.Empty;
    }
}
