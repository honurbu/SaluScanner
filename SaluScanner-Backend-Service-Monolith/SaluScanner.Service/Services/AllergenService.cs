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
    public class AllergenService : GenericService<Allergen, AllergenDto>, IAllergenService
    {
        private readonly IAllergenRepository _allergenRepository;

        public AllergenService(IGenericRepository<Allergen> _genericRepository, IUnitOfWork _unitOfWork, IAllergenRepository allergenRepository) : base(_genericRepository, _unitOfWork)
        {
            _allergenRepository = allergenRepository;
        }
    }
}
