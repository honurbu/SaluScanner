using AutoMapper;
using Microsoft.AspNetCore.Http;
using SaluScanner.Core.DTOs;
using SaluScanner.Core.Entities;
using SaluScanner.Core.Repositories;
using SaluScanner.Core.Services;
using SaluScanner.Core.UnitOfWorks;
using SaluScanner.Service.Mapping;
using SaluScanner.SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Service.Services
{
    public class UserAllergenService : GenericService<User, UserAllergenDto>, IUserAllergenService
    {
        protected readonly IUserAllergenRepository _userAllergenRepository;
        private readonly IMapper _mapper;

        public UserAllergenService(IGenericRepository<User> _genericRepository, IUnitOfWork _unitOfWork, IUserAllergenRepository _userAllergenRepository, IMapper mapper) : base(_genericRepository, _unitOfWork)
        {
            this._userAllergenRepository = _userAllergenRepository;
            _mapper = mapper;
        }

        public async Task<Response<UserAllergenDto>> GetAllergenByUser()
        {
            var entity = await _userAllergenRepository.GetAllergenByUser();
            
            if (entity == null)
            {
                return Response<UserAllergenDto>.Fail("Allergy Not Found", 404, true);
            }

            var dto = ObjectMapper.Mapper.Map<UserAllergenDto>(entity);
            return Response<UserAllergenDto>.Success(dto, 200);
        }
      



    }
}


