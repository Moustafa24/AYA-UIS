using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AYA_UIS.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Identity
{
    public class IdentityAYADbContext :IdentityDbContext
    {
        public IdentityAYADbContext(DbContextOptions<IdentityAYADbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                   .HasIndex(u => u.Academic_Code)
                   .IsUnique();
            
            builder.Entity<User>()
                   .HasIndex(u => u.UserName)
                   .IsUnique();

            // Ignore navigation properties that reference tables in the Info database
            builder.Entity<User>()
                   .Ignore(u => u.Registrations)
                   .Ignore(u => u.AcademicSchedules)
                   .Ignore(u => u.CourseUploads);

        }

        public DbSet<User> User { get; set; }
    }
}
