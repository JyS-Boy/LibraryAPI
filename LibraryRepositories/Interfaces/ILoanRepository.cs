using Library.DataAccess.Entities;

namespace Library.Repositories.Interfaces
{
    public interface ILoanRepository : IBaseRepository<Loan>
    {
        Task<Loan>CreateLoanAsync(Loan loan);
        Task<Loan?> GetLoanByIdAsync(Guid id);
    }
}
