namespace SaluScanner.Core.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }

        public string Barcode { get; set; }

        public Product()
        {
            this.Certificates = new HashSet<Certificate>();
            this.Contents = new HashSet<Content>();
        }

        // Navigations & Relations
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int NutritionId { get; set; }
        public Nutrition Nutrition { get; set; }

        public int ProductDetailId { get; set; }
        public ProductDetail ProductDetail { get; set; }

        public ICollection<Certificate>? Certificates { get; set; }

        public ICollection<Content> Contents { get; set; }
    }
}
