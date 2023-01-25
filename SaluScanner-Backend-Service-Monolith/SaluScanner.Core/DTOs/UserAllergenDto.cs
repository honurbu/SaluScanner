using SaluScanner.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.DTOs
{
    public class UserAllergenDto:UserDto
    {
        public string UserName { get; set; }
        public ICollection<Allergen> Allergies { get; set; }

    }
}
