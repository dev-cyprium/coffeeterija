using System;
using System.ComponentModel.DataAnnotations;

namespace coffeterija.application.Requests
{
    public class PagedRequest
    {
        [RegularExpression("asdf")]
        public int PerPage { get; set; }

        [RegularExpression("asdf")]
        public int Page { get; set; }

        public PagedRequest()
        {
            // PerPage = 10;
            // page = 1;
        }

    }
}
