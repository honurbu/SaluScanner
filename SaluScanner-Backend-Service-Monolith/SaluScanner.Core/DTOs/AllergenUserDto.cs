using SaluScanner.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.DTOs
{
    public class AllergenUserDto : UserDto
    {
        public string AllergenName { get; set; }
        public string? AllergenDescription { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
