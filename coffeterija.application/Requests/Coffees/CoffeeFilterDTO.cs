using System;
namespace coffeterija.application.Requests.Coffees
{
    public class CoffeeFilterDTO : PagedRequest
    {
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string ContinentName { get; set; }
        public decimal? MoreThen { get; set; }
        public decimal? LessThen { get; set; }
    }
}
