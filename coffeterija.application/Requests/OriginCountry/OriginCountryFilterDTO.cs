using System;
namespace coffeterija.application.Requests
{
    public class OriginCountryFilterDTO : PagedRequest
    {
        public string Continent { get; set; }
        public string Name { get; set; }
        public decimal? BiggerThen { get; set; }
        public decimal? SmallerThen { get; set; }
    }
}
