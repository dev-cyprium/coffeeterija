using System;
using System.Collections.Generic;

namespace coffeterija.application.Pagination
{
    public class PaginateResponse<T>
    {
        public int PerPage { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
