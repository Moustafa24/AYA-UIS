using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Info_Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    public class InfoConfigurations : IEntityTypeConfiguration<DepartmentFee>
    {
        public void Configure(EntityTypeBuilder<DepartmentFee> builder)
        {

            builder.HasOne(x => x.Department)
                   .WithMany(d => d.Fees)
                   .HasForeignKey(x => x.DepartmentId);

         builder.HasOne(x => x.GradeYear)
                .WithMany(y => y.Fees)
                .HasForeignKey(x => x.GradeYearId);

            
        }
    }
}
