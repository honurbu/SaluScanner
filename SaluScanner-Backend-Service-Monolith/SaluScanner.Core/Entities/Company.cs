using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Mail { get; set; }
        public string ContactNumber { get; set; }
        public string Website999 { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
