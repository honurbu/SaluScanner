using AutoMapper;
using SaluScanner.Core.DTOs;
using SaluScanner.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Service.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<Certificate,CertificateDto>().ReverseMap();
            CreateMap<Product,ProductWithCertificateDto>();
            CreateMap<ProductWithCertificateDto, Product>();

            CreateMap<UserDto, User>().ReverseMap();

        }
    }
}
