namespace Library.Business.DTOs.Request.Loan
{
    public class CreateLoanDetailRequest
    {
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
    }
}