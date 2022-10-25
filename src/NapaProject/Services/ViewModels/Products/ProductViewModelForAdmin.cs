using System.Text.Json.Serialization;

namespace NapaProject.Services.ViewModels.Products
{
    public class ProductViewModelForAdmin
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("item_name")]
        public string ItemName { get; set; } = String.Empty;

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("total_price_with_vat")]
        public decimal TotalPriceWithVat { get; set; }
    }
}
