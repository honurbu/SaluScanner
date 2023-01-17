using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.Entities
{
    public class Certificate : IEntity
    {
        public int Id { get; set; }
        public string CertificateName { get; set; }
        public string Description { get; set; }

        public Certificate()
        {
            this.Products = new HashSet<Product>();
        }

        public ICollection<Product> Products { get; set; }
    }
}
