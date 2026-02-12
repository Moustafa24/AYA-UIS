using AYA_UIS.Core.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    public class FeeConfiguration : IEntityTypeConfiguration<Fee>
    {
        public void Configure(EntityTypeBuilder<Fee> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Amount)
                   .HasPrecision(18, 2);

            builder.HasOne(f => f.DepartmentFee)
                   .WithMany(df => df.Fees)
                   .HasForeignKey(f => f.DepartmentFeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}