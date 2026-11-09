namespace Library.Business.DTOs.Response.Loan
{
    public class GetLoanResponse
    {
        public Guid Id { get; set; }
        public string Reader { get; set; } = string.Empty;
        public string Librarian { get; set; } = string.Empty;
        public DateTime LoanDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}