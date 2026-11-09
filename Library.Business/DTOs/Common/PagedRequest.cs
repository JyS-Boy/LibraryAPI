using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.DTOs.Common
{
    public class PagedRequest
    {
        public string? Filter { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
