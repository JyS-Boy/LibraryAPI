using Library.DataAccess;
using Library.DataAccess.Entities;
using Library.Repositories.Interfaces;

namespace Library.Repositories.Implementations;

public class LoanRepository : BaseRepository<Loan>, ILoanRepository
{
    public LoanRepository(LibraryDbContext context) : base(context)
    {

    }
}
