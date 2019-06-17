using System;
namespace coffeterija.application.Responses
{
    public class CoffeeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public string ContinentName { get; set; }
        public string CountryName { get; set; }
    }
}
