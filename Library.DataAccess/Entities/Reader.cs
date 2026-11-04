using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Entities
{
    public class Reader : BaseEntity
    {
        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<Loan> Loans { get; private set; } = new();

        public Reader()
        {
        }

        public Reader(string fullName, string email, string phoneNumber, User user)
        {
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            User = user;
            UserId = user.Id;
        }
    }
}
