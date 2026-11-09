using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.DTOs.Common
{
    public class PagedResponse<T>
    {
        public ICollection<T> Collection { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int TotalRowsPerPage { get; set; }
    }
}
