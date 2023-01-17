﻿using SaluScanner.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.DTOs
{
    public class ProductWithCertificateDto:ProductDto
    {
        public CertificateDto Certificate { get; set; }
    }
}