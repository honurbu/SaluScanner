using SaluScanner.Core.Entities;
using SaluScanner.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaluScanner.Core.Repositories;

using System.Threading.Tasks;
using SaluScanner.Repository.DbContexts;

namespace SaluScanner.Repository.Repositories
{
    public class AllergenRepository : GenericRepository<Allergen>, IAllergenRepository
    {
        public AllergenRepository(SqlServerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
