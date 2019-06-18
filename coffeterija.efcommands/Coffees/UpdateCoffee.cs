using System;
using System.Linq;
using coffeterija.application.Commands.Coffees;
using coffeterija.application.Exceptions;
using coffeterija.application.Requests.Coffees;
using coffeterija.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace coffeterija.efcommands.Coffees
{
    public class UpdateCoffee : CofeterijaBase, IUpdateCoffee
    {
        public UpdateCoffee(CoffeeContext context) : base(context) {}

        public void Execute(UpdateCoffeeDTO request)
        {
            var coffee = CoffeeContext
                .Coffees
                .Include(c => c.Images)
                .Include(c => c.Prices)
                .FirstOrDefault(c => c.Id == request.Id);

            if(coffee == null)
            {
                throw new EntityNotFoundException();
            }

            if(request.Name != null)
            {
                coffee.Name = request.Name;
            }

            if(request.Price != null)
            {
                coffee.Prices.Add(new CoffeePrice()
                {
                    Price = request.Price.Value
                });
            }

            if(request.CountryId != null)
            {
                var country = CoffeeContext.OriginCountries.FirstOrDefault(c => c.Id == request.CountryId);
                if(country == null)
                {
                    throw new EntityNotFoundException();
                }

                coffee.Country = country;
            }

            if(request.ImagePath != null)
            {
                coffee.Images.Clear();
                coffee.Images.Add(new CoffeeImage()
                {
                    ImagePath = request.ImagePath
                });
            }

            CoffeeContext.SaveChanges();
        }
    }
}
