using Library.DataAccess.Entities;

public class Reader : BaseEntity
{
    public string FullName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public List<Loan> Loans { get; private set; } = new();

    public Reader()
    {
    }

    public Reader(string fullName, string email, string phoneNumber)
    {
        FullName = fullName;

        Email = email;

        PhoneNumber = phoneNumber;
    }
}