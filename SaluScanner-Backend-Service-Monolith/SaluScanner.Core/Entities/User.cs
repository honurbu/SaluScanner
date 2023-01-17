﻿using Microsoft.AspNetCore.Identity;

namespace SaluScanner.Core.Entities
{
    public class User : IdentityUser , IEntity
    {
        public bool IsDarkMode { get; set; }

        public User()
        {
            this.Allergies = new HashSet<Allergen>();
        }

        // Navigations & Relations
        ICollection<Allergen> Allergies { get; set; }
    }
}