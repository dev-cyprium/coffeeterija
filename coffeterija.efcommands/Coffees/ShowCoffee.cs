using System;
using System.Linq;
using coffeterija.application.Commands.Coffees;
using coffeterija.application.Exceptions;
using coffeterija.application.Responses;
using coffeterija.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace coffeterija.efcommands.Coffees
{
    public class ShowCoffee : CofeterijaBase, IShowCoffee
    {
        public ShowCoffee(CoffeeContext context) : base(context)
        {
        }

        public CoffeeResponse Execute(int id)
        {
            var coffee = CoffeeContext
                .Coffees
                .Include(cof => cof.Country)
                .ThenInclude(cou => cou.Continent)
                .Include(cof => cof.Images)
                .Include(cof => cof.Prices)
                .FirstOrDefault(c => c.Id == id);

            if(coffee == null)
            {
                throw new EntityNotFoundException();
            }

            return new CoffeeResponse()
            {
                Id = coffee.Id,
                Name = coffee.Name,
                ContinentName = coffee.Country.Continent.Name,
                CountryName = coffee.Country.Name,
                ImageURL = coffee.Images.First().ImagePath,
                Price = coffee.Prices
                    .OrderByDescending(p => p.CreatedAt)
                    .First()
                    .Price
            };
        }
    }
}
