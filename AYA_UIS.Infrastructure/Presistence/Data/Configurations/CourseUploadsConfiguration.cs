using AYA_UIS.Core.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    public class CourseUploadsConfiguration : IEntityTypeConfiguration<CourseUploads>
    {
        public void Configure(EntityTypeBuilder<CourseUploads> builder)
        {
            builder.HasKey(cu => cu.Id);

            builder.HasOne(cu => cu.Course)
                   .WithMany(c => c.CourseUploads)
                   .HasForeignKey(cu => cu.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cu => cu.UploadedBy)
                   .WithMany(u => u.CourseUploads)
                   .HasForeignKey(cu => cu.UploadedByUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(cu => cu.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(cu => cu.Type)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(cu => cu.FileId)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(cu => cu.Url)
                   .IsRequired()
                   .HasMaxLength(500);
        }
    }
}
