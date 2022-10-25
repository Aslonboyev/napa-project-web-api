namespace NapaProject.Models
{
    public class ProductAudit
    {
        public long Id { get; set; }

        public long ChangedBy { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public DateTime ChangedTime { get; set; }

        public string Changed { get; set; } = String.Empty;
    }
}