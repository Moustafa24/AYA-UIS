using AYA_UIS.Core.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    public class AcademicScheduleConfiguration : IEntityTypeConfiguration<AcademicSchedule>
    {
        public void Configure(EntityTypeBuilder<AcademicSchedule> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Department)
                   .WithMany(d => d.AcademicSchedules)
                   .HasForeignKey(a => a.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // User lives in a separate Identity database, so no FK constraint
            builder.Ignore(a => a.UploadedBy);

            builder.Property(a => a.UploadedByUserId)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(a => a.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(a => a.FileId)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(a => a.Url)
                   .IsRequired()
                   .HasMaxLength(500);
        }
    }
}