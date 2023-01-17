using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.Entities
{
    public class Content : IEntity
    {
        public int Id { get; set; }
        public string ComponentName { get; set; }
        public string? ComponentDescription { get; set; }
        public bool IsAnimalProduct { get; set; }

        public Content()
        {
            this.Products = new HashSet<Product>();
        }

        public int? AllergenId { get; set; }
        public Allergen? Allergen { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
