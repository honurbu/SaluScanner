using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.DTOs
{
    public class NonUserLoginDto : IDto
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
