using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Presistence.Identity
{
    public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityAYADbContext>
    {
        public IdentityAYADbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityAYADbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=AYA_Database;Trusted_Connection=True;TrustServerCertificate=True;");

            return new IdentityAYADbContext(optionsBuilder.Options);
        }
    }
}
