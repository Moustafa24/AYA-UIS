using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AYA_UIS.Core.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    public class InfoConfigurations : IEntityTypeConfiguration<DepartmentFee>
    {
        public void Configure(EntityTypeBuilder<DepartmentFee> builder)
        {
            builder.HasKey(df => df.Id);

            builder.HasOne(df => df.Department)
                   .WithMany(d => d.DepartmentFees)
                   .HasForeignKey(df => df.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(df => df.StudyYear)
                   .WithMany(sy => sy.DepartmentFees)
                   .HasForeignKey(df => df.StudyYearId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(df => df.Fees)
                   .WithOne(f => f.DepartmentFee)
                   .HasForeignKey(f => f.DepartmentFeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
