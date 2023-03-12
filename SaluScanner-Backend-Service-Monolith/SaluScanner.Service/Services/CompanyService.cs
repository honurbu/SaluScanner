using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SaluScanner.Core.DTOs;
using SaluScanner.Core.Entities;
using SaluScanner.Core.Repositories;
using SaluScanner.Core.Services;
using SaluScanner.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Service.Services
{
    public class CompanyService : GenericService<Company, CompanyDto>, ICompanyService
    {
        protected readonly ICompanyRepository _repository;
        private readonly IMapper _mapper;

        public CompanyService(IGenericRepository<Company> _genericRepository, ICompanyRepository repository,IUnitOfWork _unitOfWork, IMapper mapper) : base(_genericRepository, _unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
        }


        //public UserAllergenService(IGenericRepository<User> _genericRepository, IUnitOfWork _unitOfWork, IUserAllergenRepository _userAllergenRepository, IMapper mapper) : base(_genericRepository, _unitOfWork)
        //{
        //    this._userAllergenRepository = _userAllergenRepository;
        //    _mapper = mapper;
        //}




    }
}
