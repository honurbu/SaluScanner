using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.Entities
{
    public class ProductDetail : IEntity
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string ProductOrigin { get; set; }
        public string? Image { get; set; }
    }
}