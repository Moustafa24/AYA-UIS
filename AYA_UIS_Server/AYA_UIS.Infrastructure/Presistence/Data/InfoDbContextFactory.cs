using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Presistence.Data
{
    public class InfoDbContextFactory : IDesignTimeDbContextFactory<AYA_UIS_InfoDbContext>
    {
        public AYA_UIS_InfoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AYA_UIS_InfoDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=Info_Database;Trusted_Connection=True;TrustServerCertificate=True;");

            return new AYA_UIS_InfoDbContext(optionsBuilder.Options);
        }
    }
}
