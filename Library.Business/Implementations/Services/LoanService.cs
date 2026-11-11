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


        public Task<Result<GetLoanResponse>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<Result<List<ListLoansResponse>>> GetAllAsync()
        {
            throw new NotImplementedException();
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

            loan.AddTracking(new LoanTracking(StatusLoan.Returned));

            await _loanRepository.UpdateAsync(loan);

            return Result.Success("Préstamo devuelto correctamente.");
        }
    }
}
