using Library.DataAccess.Emuns;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Entities
{
    public class LoanTracking : BaseEntity
    {
        public Guid LoanId { get; private set; }
        public Loan? Loan { get; private set; }
        public StatusLoan Status { get; private set; }
        public DateTime DueStatus { get; private set; }
        
        public LoanTracking()
        {
        }
        public LoanTracking( Loan loan, StatusLoan status)
        {
            LoanId = loan.Id;
            Loan = loan;
            DueStatus = DateTime.UtcNow;
            Status = status;
        }

        public LoanTracking(StatusLoan status)
        {
            DueStatus = DateTime.UtcNow;
            Status = status;
        }
    }
}
