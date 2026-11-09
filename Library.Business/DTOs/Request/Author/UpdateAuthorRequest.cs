using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.DTOs.Request.Author
{
    public class UpdateAuthorRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
