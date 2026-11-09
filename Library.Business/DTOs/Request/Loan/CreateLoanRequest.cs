namespace Library.Business.DTOs.Request.Loan
{
    public class CreateLoanRequest
    {
        public Guid ReaderId { get; set; }
        public string Librarian { get; set; } = string.Empty;
        public DateTime ExpectedReturnDate { get; set; }
        public List<CreateLoanDetailRequest> Details { get; set; } = [];
    }
}