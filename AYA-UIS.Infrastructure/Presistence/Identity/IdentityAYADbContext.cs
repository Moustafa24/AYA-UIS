using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Identity;
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

        }

        public DbSet<User> User { get; set; }
    }
}
