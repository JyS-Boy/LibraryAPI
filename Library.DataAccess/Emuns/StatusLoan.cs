using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Emuns
{
    public enum StatusLoan
    {
        Registered = 1,
        Borrowed = 2,
        Returned = 3,
        Overdue = 4,
        Canceled = 5
    }
}
