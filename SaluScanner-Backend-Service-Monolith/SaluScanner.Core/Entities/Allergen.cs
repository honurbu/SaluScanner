using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.Entities
{
    public class Allergen : IEntity
    {
        public int Id { get; set; }
        public string AllergenName { get; set; }
        public string? AllergenDescription { get; set; }
    }
}
