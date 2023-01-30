using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class UserAllergenRepository : GenericRepository<User>, IUserAllergenRepository
    {
        public UserAllergenRepository(SqlServerDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetAllergenByUser()
        {


            var userAllergy = dbContext.Users
                .Include(x => x.Allergies);

            return await userAllergy.FirstOrDefaultAsync();


        }
    }
}

