using Library.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess
{
    public class LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(LibraryDbContext).Assembly);
        }
    }
}