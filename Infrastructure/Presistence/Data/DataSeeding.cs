using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Data
{
    public class DataSeeding (AYA_UIS_InfoDbContext _dbContext) : IDataSeeding
    {
        public async Task SeedDataInfoAsync()
        {
            var pendingMigration = await _dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigration.Any())
            {
                await _dbContext.Database.MigrateAsync();
            }
        }
    }
}
