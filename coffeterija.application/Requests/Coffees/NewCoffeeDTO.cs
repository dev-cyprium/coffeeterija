using System;
using System.ComponentModel.DataAnnotations;

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
    }
}
