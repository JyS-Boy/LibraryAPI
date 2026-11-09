namespace Library.Business.DTOs.Response.Loan
{
    public class ListLoansResponse
    {
        public Guid Id { get; set; }
        public string Reader { get; set; } = string.Empty;
        public DateTime LoanDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}