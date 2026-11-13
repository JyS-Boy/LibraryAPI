using Library.Business.DTOs.Request.Loan;
using Library.Business.DTOs.Response.Loan;
using Library.Business.Interfaces.Services;
using Library.Common.Helpers;
using Library.DataAccess.Emuns;
using Library.DataAccess.Entities;
using Library.Repositories.Interfaces;

namespace Library.Business.Implementations.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IReaderRepository _readerRepository;
        public LoanService(ILoanRepository loanRepository, IBookRepository bookRepository, IReaderRepository readerRepository)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _readerRepository = readerRepository;
        }
        public async Task<Result> CreateAsync(CreateLoanRequest request)
        {
            var reader = await _readerRepository.GetByIdAsync(request.ReaderId);

            if (reader is null)
                return Result.Failure($"Lector con ID {request.ReaderId} no encontrado.");

            if (!request.Details.Any())
                return Result.Failure("El préstamo debe contener al menos un libro.");

            var ids = request.Details
                .Select(x => x.BookId)
                .ToList();

            var books = await _bookRepository.GetByListIdsAsync(ids);

            if (books.Count != ids.Count)
                return Result.Failure("Uno o más libros no existen.");

            foreach (var detail in request.Details)
            {
                if (detail.Quantity < 1 || detail.Quantity > 2)
                {
                    return Result.Failure("La cantidad debe estar entre 1 y 2.");
                }

                var book = books.First(
                    b => b.Id == detail.BookId);

                if (book.Stock < detail.Quantity)
                {
                    return Result.Failure($"Stock insuficiente para el libro '{book.Title}'.");
                }
            }

            foreach (var detail in request.Details)
            {
                var book = books.First(b => 
                        b.Id == detail.BookId);

                book.DecreaseStock(detail.Quantity);
                await _bookRepository.UpdateAsync(book);
            }

            var details = new List<LoanDetail>();

            foreach (var detail in request.Details)
            {
                var book = books.First(b =>
                        b.Id == detail.BookId);
                details.Add(new LoanDetail(book, detail.Quantity));
            }

            var tracking = new List<LoanTracking>
            {
                new LoanTracking(StatusLoan.Registered)
            };

            var loan = new Loan(reader, request.Librarian, request.ExpectedReturnDate, details, tracking);

            if (request.ExpectedReturnDate <= DateTime.UtcNow)
            {
                return Result.Failure("La fecha esperada de devolución debe ser mayor a la fecha actual.");
            }

            await _loanRepository.CreateLoanAsync(loan);

            return Result.Success("Préstamo registrado correctamente.");
        }


        public async Task<Result<GetLoanResponse>> GetByIdAsync(Guid id)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(id);

            if (loan is null)
            {
                return Result.Failure<GetLoanResponse>($"Préstamo con id {id} no encontrado.");
            }

            var status =
                loan.Tracking
                    .OrderByDescending(x => x.DueStatus)
                    .First()
                    .Status
                    .ToString();

            return Result.Success(
                new GetLoanResponse
                {
                    Id = loan.Id,
                    Reader = loan.Reader?.FullName ?? "",
                    Librarian = loan.Librarian,
                    LoanDate = loan.LoanDate,
                    ExpectedReturnDate = loan.ExpectedReturnDate,
                    Status = status
                });
        }

        public async Task<Result<List<ListLoansResponse>>> GetAllAsync()
        {
            var loans = await _loanRepository.GetAllLoansAsync();

            var response = loans.Select(x =>
                    new ListLoansResponse
                    {
                        Id = x.Id,
                        Reader = x.Reader?.FullName ?? "",
                        LoanDate = x.LoanDate,
                        Status = x.Tracking
                            .OrderByDescending(t => t.DueStatus)
                            .First()
                            .Status
                            .ToString()
                    }).ToList();

            return Result.Success(response);
        }
        public async Task<Result> ReturnLoanAsync(Guid id)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(id);

            if (loan is null)
            {
                return Result.Failure($"Préstamo con id {id} no encontrado.");
            }

            var alreadyReturned = loan.Tracking.Any(x =>
                    x.Status == StatusLoan.Returned);

            if (alreadyReturned)
            {
                return Result.Failure("El préstamo ya ha sido devuelto.");
            }

            foreach (var detail in loan.Details)
            {
                if (detail.Book is null)
                    continue;

                detail.Book.IncreaseStock(detail.Quantity);

                await _bookRepository.UpdateAsync(detail.Book);
            }

            var tracking = new LoanTracking(loan, StatusLoan.Returned);

            await _loanRepository.AddTrackingAsync(tracking);

            return Result.Success("Préstamo devuelto correctamente.");
        }
    }
}
