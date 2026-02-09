using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AYA_UIS.Core.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Presistence.Data.Configurations;

namespace Presistence.Data
{
    public class AYA_UIS_InfoDbContext : DbContext 
    {

        public AYA_UIS_InfoDbContext(DbContextOptions<AYA_UIS_InfoDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<StudyYear> StudyYears { get; set; }
        public DbSet<DepartmentFee> DepartmentFees { get; set; }
        public DbSet<AcademicSchedule> AcademicSchedules { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<CourseUploads> CourseUploads { get; set; }

    }
}
