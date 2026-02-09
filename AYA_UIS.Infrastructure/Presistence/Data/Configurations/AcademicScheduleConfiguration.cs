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

            builder.HasOne(a => a.UploadedBy)
                   .WithMany(u => u.AcademicSchedules)
                   .HasForeignKey(a => a.UploadedByUserId)
                   .OnDelete(DeleteBehavior.Restrict);

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