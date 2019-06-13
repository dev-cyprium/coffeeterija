using System;
using System.ComponentModel.DataAnnotations;

namespace coffeterija.application.Requests
{
    public class PagedRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Per page must be at least 1")]
        public int PerPage { get; set; } = 10;

        [Range(1, int.MaxValue, ErrorMessage = "Per page must be at least 1")]
        public int Page { get; set; } = 1;
    }
}
