using Library.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Library.DataAccess.Configurations
{
    public class LoanTrackingConfigurations : IEntityTypeConfiguration<LoanTracking>
    {
        public void Configure(EntityTypeBuilder<LoanTracking> builder)
        {
            builder.HasKey(lt => lt.Id);

            builder.Property(lt => lt.Status)
                .IsRequired();

            builder.Property(lt => lt.DueStatus)
                .IsRequired();

            builder.HasOne(lt => lt.Loan)
                .WithMany(l => l.Tracking)
                .HasForeignKey(lt => lt.LoanId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
