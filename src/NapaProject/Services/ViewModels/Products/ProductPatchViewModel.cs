using System.Text.Json.Serialization;

namespace NapaProject.Services.ViewModels.Products
{
    public class ProductPatchViewModel
    {
        [JsonPropertyName("item_name")]
        public string? ItemName { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
