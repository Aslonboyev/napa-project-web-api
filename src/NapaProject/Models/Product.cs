namespace NapaProject.Models
{
    public class Product
    {
        public long Id { get; set; }

        public string ItemName { get; set; } = String.Empty;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPriceWithVat { get; set; }
    }
}
