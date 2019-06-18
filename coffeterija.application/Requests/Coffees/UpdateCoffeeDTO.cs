using System;
using Microsoft.AspNetCore.Http;

namespace coffeterija.application.Requests.Coffees
{
    public class UpdateCoffeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? CountryId { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
    }
}
