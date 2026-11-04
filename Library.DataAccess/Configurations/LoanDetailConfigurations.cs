using Library.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataAccess.Configurations
{
    public class LoanDetailConfigurations : IEntityTypeConfiguration<LoanDetail>
    {
        public void Configure(EntityTypeBuilder<LoanDetail> builder)
        {
            builder.HasKey(ld => ld.Id);

            builder.Property(ld => ld.Quantity)
                .IsRequired();

            builder.HasOne(ld => ld.Loan)
                .WithMany(l => l.Details)
                .HasForeignKey(ld => ld.LoanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ld => ld.Book)
                .WithMany(b => b.LoanDetails)
                .HasForeignKey(ld => ld.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
