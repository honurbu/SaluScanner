﻿using SaluScanner.Core.UnitOfWorks;
using SaluScanner.Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlServerDbContext dbContext;

        public UnitOfWork(SqlServerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
