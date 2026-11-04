using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Role { get; private set; } = string.Empty;
        public User()
        {
        }
        public User(string userName, string passwordHash, string role)
        {
            UserName = userName;
            PasswordHash = passwordHash;
            Role = role;
        }
    }
}
