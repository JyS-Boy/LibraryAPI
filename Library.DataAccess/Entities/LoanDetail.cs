using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Entities
{
    public class LoanDetail : BaseEntity
    {
        public Guid LoanId { get; private set; }
        public Loan? Loan { get; private set; }
        public Guid BookId { get; private set; }
        public Book? Book { get; private set; }
        public int Quantity { get; private set; }
        public LoanDetail()
        {
        }
        public LoanDetail(Book book, int quantity)
        {
            BookId = book.Id;
            Book = book;
            Quantity = quantity;
        }
        public LoanDetail( Loan loan, Book book, int quantity)
        {
            LoanId = loan.Id;
            Loan = loan;
            BookId = book.Id;
            Book = book;
            Quantity = quantity;
        }
    }
}
