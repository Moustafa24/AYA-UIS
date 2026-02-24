using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Presistence
{
    public class UniversityDbContextFactory : IDesignTimeDbContextFactory<UniversityDbContext>
    {
        public UniversityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniversityDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=AYA_Database;Trusted_Connection=True;TrustServerCertificate=True");

            return new UniversityDbContext(optionsBuilder.Options);
        }
    }
}
