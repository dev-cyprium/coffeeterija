using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace coffeterija.application.Requests.Coffees
{
    public class NewCoffeeDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public string ImagePath { get; set; }
    }
}
