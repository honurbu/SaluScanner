using SaluScanner.Core.Entities;
using SaluScanner.Core.Repositories;
using SaluScanner.Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Repository.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(SqlServerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
