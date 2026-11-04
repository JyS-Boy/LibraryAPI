using Library.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataAccess.Configurations
{
    public class LoanConfigurations : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Librarian)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(l => l.LoanDate)
                .IsRequired();

            builder.Property(l => l.ExpectedReturnDate)
                .IsRequired();

            builder.HasOne(l => l.Reader)
                .WithMany(r => r.Loans)
                .HasForeignKey(l => l.ReaderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
