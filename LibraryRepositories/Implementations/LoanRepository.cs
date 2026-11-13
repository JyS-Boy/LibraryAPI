using Library.DataAccess;
using Library.DataAccess.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Implementations;

public class LoanRepository : BaseRepository<Loan>, ILoanRepository
{
    public LoanRepository(LibraryDbContext context) : base(context)
    {
    }

    public async Task<Loan> CreateLoanAsync(Loan loan)
    {
        await _context.Set<Loan>().AddAsync(loan);
        await _context.SaveChangesAsync();
        return loan;
    }

    public async Task<Loan?> GetLoanByIdAsync(Guid id)
    {
        return await _context.Set<Loan>()
            .Include(x => x.Reader)
            .Include(x => x.Details)
                .ThenInclude(x => x.Book)
            .Include(x => x.Tracking)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Loan>> GetAllLoansAsync()
    {
        return await _context.Set<Loan>()
            .Include(x => x.Reader)
            .Include(x => x.Tracking)
            .ToListAsync();
    }

    public async Task AddTrackingAsync(LoanTracking tracking)
    {
        await _context.Set<LoanTracking>().AddAsync(tracking);

        await _context.SaveChangesAsync();
    }

}
