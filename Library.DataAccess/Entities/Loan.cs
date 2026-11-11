using Library.DataAccess.Emuns;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Entities
{
    public class Loan : BaseEntity
    {
        public Guid ReaderId { get; private set; }
        public Reader? Reader { get; private set; }
        public string Librarian { get; private set; } = string.Empty;
        public DateTime LoanDate { get; private set; }
        public DateTime ExpectedReturnDate { get; private set; }
        public List<LoanDetail> Details { get; private set; } = new();
        public List<LoanTracking> Tracking { get; private set; } = new();
        public Loan ()
        {
        }

        public Loan(Reader reader, string librarian, DateTime expectedReturnDate, List<LoanDetail> details, List<LoanTracking> tracking)
        {
            ReaderId = reader.Id;
            Reader = reader;
            Librarian = librarian;
            LoanDate = DateTime.UtcNow;
            ExpectedReturnDate = expectedReturnDate;
            Details = details;
            Tracking = tracking;
        }

        public void AddTracking(LoanTracking tracking)
        {
            Tracking.Add(tracking);
        }
        public void AddDetail(LoanDetail detail)
        {
            Details.Add(detail);
        }

        public bool IsReturned()
        {
            return Tracking.Any(x => x.Status == StatusLoan.Returned);
        }
        public void ReturnLoan()
        {
            Tracking.Add(new LoanTracking(StatusLoan.Returned));
        }
    }
}
