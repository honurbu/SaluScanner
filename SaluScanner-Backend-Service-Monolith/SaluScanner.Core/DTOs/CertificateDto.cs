using SaluScanner.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.DTOs
{
    public class CertificateDto : IDto
    {
        public int Id { get; set; }
        public string CertificateName { get; set; }
        public string Description { get; set; }

    }
}
