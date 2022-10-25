namespace NapaProject.Services.ViewModels.Products
{
    public class ProductViewModel
    {
        public long Id { get; set; }

        public string ItemName { get; set; } = String.Empty;

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}