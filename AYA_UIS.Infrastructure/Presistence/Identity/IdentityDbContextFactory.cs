using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Presistence.Identity
{
    public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityAYADbContext>
    {
        public IdentityAYADbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityAYADbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=university_user_db;User Id=sa;Password=Yossef_2004;TrustServerCertificate=True;");

            return new IdentityAYADbContext(optionsBuilder.Options);
        }
    }
}
