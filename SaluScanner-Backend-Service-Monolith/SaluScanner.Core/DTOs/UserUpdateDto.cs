﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.DTOs
{
    public class UserUpdateDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public float? weight { get; set; }
        public float? height { get; set; }
    }
}
