using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.DTOs
{
    public class CompanyDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Mail { get; set; }
        public string ContactNumber { get; set; }
        public string Website999 { get; set; }
    }
}
