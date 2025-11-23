using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Info_Module;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Data
{
    public class AYA_UIS_InfoDbContext : DbContext 
    {

        public AYA_UIS_InfoDbContext(DbContextOptions<AYA_UIS_InfoDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyRefrence).Assembly);
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<GradeYear> GradeYears { get; set; }
        public DbSet<DepartmentFee> DepartmentFees { get; set; }
        public DbSet<AcademicSchedules> AcademicSchedules { get; set; }


    }
}
