using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.DTOs
{
    public class UserDto : IDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsDarkMode { get; set; }

    }
}
