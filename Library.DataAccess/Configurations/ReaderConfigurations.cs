using Library.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataAccess.Configurations
{
    public class ReaderConfigurations : IEntityTypeConfiguration<Reader>
    {
        public void Configure(EntityTypeBuilder<Reader> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.FullName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(r => r.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(r => r.Email)
                .IsUnique();

            builder.HasOne(r => r.User)
                .WithOne()
                .HasForeignKey<Reader>(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
