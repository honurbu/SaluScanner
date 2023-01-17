namespace SaluScanner.Core.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        // Navigations & Relations
        public ICollection<Product> Products { get; set; }
    }
}
