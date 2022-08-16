using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable(name: "Course");

            builder.Property(p => p.Name)
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();

            builder.Property(p => p.Code)
                .HasColumnType("NVARCHAR(15)")
                .IsRequired();
        }
    }
}
