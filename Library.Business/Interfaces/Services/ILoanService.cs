using Library.Business.DTOs.Response.Loan;
using Library.Business.DTOs.Request.Loan;
using Library.Common.Helpers;

namespace Library.Business.Interfaces.Services
{
    public interface ILoanService
    {
        Task<Result> CreateAsync(
            CreateLoanRequest request);

        Task<Result<GetLoanResponse>>
            GetByIdAsync(Guid id);

        Task<Result<List<ListLoansResponse>>>
            GetAllAsync();

        Task<Result> ReturnLoanAsync(Guid id);
    }
}