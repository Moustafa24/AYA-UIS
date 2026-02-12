using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Presistence.Data
{
    public class InfoDbContextFactory : IDesignTimeDbContextFactory<AYA_UIS_InfoDbContext>
    {
        public AYA_UIS_InfoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AYA_UIS_InfoDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=university_db;User Id=sa;Password=Yossef_2004;TrustServerCertificate=True;");

            return new AYA_UIS_InfoDbContext(optionsBuilder.Options);
        }
    }
}
