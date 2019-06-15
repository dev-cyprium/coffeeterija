using System;
namespace coffeterija.application.Requests
{
    public class OriginCountryFilterDTO : PagedRequest
    {
        public string Country { get; set; }
        public string Name { get; set; }
    }
}
