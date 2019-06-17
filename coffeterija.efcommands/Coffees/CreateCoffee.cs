using System;
using System.Collections.Generic;
using System.Linq;
using coffeterija.application.Commands.Coffees;
using coffeterija.application.Exceptions;
using coffeterija.application.Requests.Coffees;
using coffeterija.dataaccess;

namespace coffeterija.efcommands.Coffees
{
    public class CreateCoffee : CofeterijaBase, ICreateCoffee
    {
        public CreateCoffee(CoffeeContext context) : base(context)
        {}

        public void Execute(NewCoffeeDTO request)
        {
            OriginCountry country = CoffeeContext.OriginCountries.FirstOrDefault(c => c.Id == request.CountryId);
            CoffeePrice price = new CoffeePrice()
            {
                Price = request.Price.Value
            };

            Coffee coffee = new Coffee()
            {
                Name = request.Name
            };

            CoffeeImage image = new CoffeeImage()
            {
                ImagePath = request.ImagePath   
            };

            List<CoffeePrice> prices = new List<CoffeePrice>();
            List<CoffeeImage> images = new List<CoffeeImage>();

            prices.Add(price);
            images.Add(image);

            coffee.Prices = prices;
            coffee.Images = images;
            coffee.Country = country ?? throw new EntityNotFoundException();

            CoffeeContext.Coffees.Add(coffee);
            CoffeeContext.SaveChanges();
        }
    }
}
