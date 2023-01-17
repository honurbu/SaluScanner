using SaluScanner.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.DTOs
{
    public class ProductDto : IDto
    {
        public string Barcode { get; set; }
        public ProductDetail ProductDetail { get; set; }
        public List<CertificateDto>? Certificates { get; set; }

    }
}
